
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.DTOs.Seats;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Services;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _repository;

    public SeatService(ISeatRepository repository)
    {
        _repository = repository;
    }

    public async Task<SeatResponse?> CreateAsync(
        CreateSeatRequest request)
    {
        var screenExists = await _repository
            .ScreenExistsAsync(request.ScreenId);

        if (!screenExists)
            return null;

        var rowName = request.RowName.Trim().ToUpperInvariant();

        var duplicate = await _repository.SeatExistsAsync(
            request.ScreenId,
            rowName,
            request.SeatNumber);

        if (duplicate)
            throw new InvalidOperationException(
                $"Seat {rowName}{request.SeatNumber} already exists in this screen.");

        var seat = new Seat
        {
            ScreenId = request.ScreenId,
            RowName = rowName,
            SeatNumber = request.SeatNumber,
            SeatType = ParseSeatType(request.SeatType)
        };

        var created = await _repository.CreateAsync(seat);

        return MapToResponse(created);
    }

    public async Task<List<SeatResponse>?> CreateBulkAsync(
        BulkCreateSeatsRequest request)
    {
        var screenExists = await _repository
            .ScreenExistsAsync(request.ScreenId);

        if (!screenExists)
            return null;

        var normalizedSeats = request.Seats.Select(s => new
        {
            RowName = s.RowName.Trim().ToUpperInvariant(),
            s.SeatNumber,
            s.SeatType
        }).ToList();

        var duplicatesInRequest = normalizedSeats
            .GroupBy(s => new { s.RowName, s.SeatNumber })
            .Any(g => g.Count() > 1);

        if (duplicatesInRequest)
            throw new InvalidOperationException(
                "The request contains duplicate seat coordinates.");

        var seatsToCreate = new List<Seat>();

        foreach (var item in normalizedSeats)
        {
            var exists = await _repository.SeatExistsAsync(
                request.ScreenId,
                item.RowName,
                item.SeatNumber);

            if (exists)
                throw new InvalidOperationException(
                    $"Seat {item.RowName}{item.SeatNumber} already exists.");

            seatsToCreate.Add(new Seat
            {
                ScreenId = request.ScreenId,
                RowName = item.RowName,
                SeatNumber = item.SeatNumber,
                SeatType = ParseSeatType(item.SeatType)
            });
        }

        var createdSeats = await _repository.CreateBulkAsync(seatsToCreate);

        return createdSeats.Select(MapToResponse).ToList();
    }

    public async Task<List<SeatResponse>?> GetByScreenIdAsync(int screenId)
    {
        var screenExists = await _repository.ScreenExistsAsync(screenId);

        if (!screenExists)
            return null;

        var seats = await _repository.GetByScreenIdAsync(screenId);

        return seats.Select(MapToResponse).ToList();
    }

    public async Task<SeatResponse?> GetByIdAsync(int id)
    {
        var seat = await _repository.GetByIdAsync(id);

        return seat == null ? null : MapToResponse(seat);
    }

    public async Task<SeatResponse?> UpdateAsync(
        int id,
        UpdateSeatRequest request)
    {
        var seat = await _repository.GetByIdAsync(id);

        if (seat == null)
            return null;

        var screenExists = await _repository
            .ScreenExistsAsync(request.ScreenId);

        if (!screenExists)
            return null;

        var rowName = request.RowName.Trim().ToUpperInvariant();

        var duplicate = await _repository.SeatExistsAsync(
            request.ScreenId,
            rowName,
            request.SeatNumber,
            id);

        if (duplicate)
            throw new InvalidOperationException(
                $"Seat {rowName}{request.SeatNumber} already exists.");

        seat.ScreenId = request.ScreenId;
        seat.RowName = rowName;
        seat.SeatNumber = request.SeatNumber;
        seat.SeatType = ParseSeatType(request.SeatType);

        var updated = await _repository.UpdateAsync(seat);

        return MapToResponse(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var seat = await _repository.GetByIdAsync(id);

        if (seat == null)
            return false;

        try
        {
            return await _repository.DeleteAsync(seat);
        }
        catch (DbUpdateException)
        {
            // Seat has dependent show or booking history.
            return false;
        }
    }

    private static SeatType ParseSeatType(string value)
    {
        if (!Enum.TryParse<SeatType>(
                value,
                ignoreCase: true,
                out var seatType) ||
            !Enum.IsDefined(typeof(SeatType), seatType))
        {
            throw new ArgumentException(
                "SeatType must be Regular or Premium.");
        }

        return seatType;
    }

    private static SeatResponse MapToResponse(Seat seat)
    {
        return new SeatResponse
        {
            SeatId = seat.SeatId,
            ScreenId = seat.ScreenId,
            RowName = seat.RowName,
            SeatNumber = seat.SeatNumber,
            SeatType = seat.SeatType.ToString()
        };
    }
}
