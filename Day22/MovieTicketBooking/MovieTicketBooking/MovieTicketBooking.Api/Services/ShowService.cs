
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.DTOs.Shows;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Services;

public class ShowService : IShowService
{
    private readonly IShowRepository _repository;

    public ShowService(IShowRepository repository)
    {
        _repository = repository;
    }

    public async Task<ShowResponse> CreateAsync(
        CreateShowRequest request)
    {
        ValidateTimeAndPrice(
            request.StartTime,
            request.EndTime,
            request.TicketPrice);

        var movieExists = await _repository
            .MovieExistsAsync(request.MovieId);

        if (!movieExists)
            throw new KeyNotFoundException(
                $"Movie with ID {request.MovieId} was not found.");

        var screenExists = await _repository
            .ScreenExistsAsync(request.ScreenId);

        if (!screenExists)
            throw new KeyNotFoundException(
                $"Screen with ID {request.ScreenId} was not found.");

        var seats = await _repository
            .GetSeatsByScreenIdAsync(request.ScreenId);

        if (seats.Count == 0)
            throw new InvalidOperationException(
                "Cannot schedule a show because this screen has no physical seats.");

        var hasOverlap = await _repository.HasOverlappingShowAsync(
            request.ScreenId,
            request.StartTime,
            request.EndTime);

        if (hasOverlap)
            throw new InvalidOperationException(
                "Another show is already scheduled during this time on this screen.");

        var show = new Show
        {
            MovieId = request.MovieId,
            ScreenId = request.ScreenId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            TicketPrice = request.TicketPrice
        };

        var showSeats = seats.Select(seat => new ShowSeat
        {
            Show = show,
            SeatId = seat.SeatId,
            Status = ShowSeatStatus.Available
        }).ToList();

        var created = await _repository.CreateWithSeatsAsync(
            show,
            showSeats);

        return MapToResponse(created);
    }

    public async Task<List<ShowResponse>> GetAllAsync(
        int? movieId,
        int? theatreId,
        DateTime? date)
    {
        var shows = await _repository.GetAllAsync(
            movieId,
            theatreId,
            date);

        return shows.Select(MapToResponse).ToList();
    }

    public async Task<ShowResponse?> GetByIdAsync(int id)
    {
        var show = await _repository.GetByIdAsync(id);

        return show == null ? null : MapToResponse(show);
    }

    public async Task<List<ShowResponse>?> GetByMovieIdAsync(int movieId)
    {
        var movieExists = await _repository
            .MovieExistsForFilterAsync(movieId);

        if (!movieExists)
            return null;

        var shows = await _repository.GetByMovieIdAsync(movieId);

        return shows.Select(MapToResponse).ToList();
    }

    public async Task<List<ShowResponse>?> GetByTheatreIdAsync(
        int theatreId)
    {
        var theatreExists = await _repository
            .TheatreExistsAsync(theatreId);

        if (!theatreExists)
            return null;

        var shows = await _repository.GetByTheatreIdAsync(theatreId);

        return shows.Select(MapToResponse).ToList();
    }

    public async Task<ShowResponse?> UpdateAsync(
        int id,
        UpdateShowRequest request)
    {
        ValidateTimeAndPrice(
            request.StartTime,
            request.EndTime,
            request.TicketPrice);

        var show = await _repository.GetByIdAsync(id);

        if (show == null)
            return null;

        var hasOverlap = await _repository.HasOverlappingShowAsync(
            show.ScreenId,
            request.StartTime,
            request.EndTime,
            id);

        if (hasOverlap)
            throw new InvalidOperationException(
                "Another show is already scheduled during this time on this screen.");

        show.StartTime = request.StartTime;
        show.EndTime = request.EndTime;
        show.TicketPrice = request.TicketPrice;

        await _repository.UpdateAsync(show);

        return MapToResponse(show);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var show = await _repository.GetByIdAsync(id);

        if (show == null)
            return false;

        var hasBookings = await _repository.HasBookingsAsync(id);

        if (hasBookings)
            throw new InvalidOperationException(
                "This show cannot be deleted because booking records already exist.");

        try
        {
            return await _repository.DeleteAsync(show);
        }
        catch (DbUpdateException)
        {
            return false;
        }
    }

    private static void ValidateTimeAndPrice(
        DateTime startTime,
        DateTime endTime,
        decimal ticketPrice)
    {
        if (endTime <= startTime)
            throw new ArgumentException(
                "EndTime must be later than StartTime.");

        if (ticketPrice < 0)
            throw new ArgumentException(
                "TicketPrice cannot be negative.");
    }

    private static ShowResponse MapToResponse(Show show)
    {
        return new ShowResponse
        {
            ShowId = show.ShowId,
            MovieId = show.MovieId,
            ScreenId = show.ScreenId,
            StartTime = show.StartTime,
            EndTime = show.EndTime,
            TicketPrice = show.TicketPrice
        };
    }

    public async Task<List<ShowSeatResponse>?> GetSeatLayoutAsync(int showId)
    {
        var show = await _repository.GetByIdAsync(showId);

        if (show == null)
            return null;

        var showSeats =
            await _repository.GetSeatAvailabilityByShowIdAsync(showId);

        return showSeats.Select(ss => new ShowSeatResponse
        {
            ShowSeatId = ss.ShowSeatId,
            SeatId = ss.SeatId,
            RowName = ss.Seat!.RowName,
            SeatNumber = ss.Seat.SeatNumber,
            SeatLabel = $"{ss.Seat.RowName}{ss.Seat.SeatNumber}",
            SeatType = ss.Seat.SeatType.ToString(),
            Price = show.TicketPrice,
            Status = ss.Status.ToString()
        }).ToList();
    }

    public async Task<List<ShowSeatResponse>?> GetAvailableSeatsAsync(int showId)
    {
        var seats = await GetSeatLayoutAsync(showId);

        return seats?
            .Where(s => s.Status == ShowSeatStatus.Available.ToString())
            .ToList();
    }

    public async Task<List<ShowSeatResponse>?> GetBookedSeatsAsync(int showId)
    {
        var seats = await GetSeatLayoutAsync(showId);

        return seats?
            .Where(s => s.Status == ShowSeatStatus.Booked.ToString())
            .ToList();
    }
}
