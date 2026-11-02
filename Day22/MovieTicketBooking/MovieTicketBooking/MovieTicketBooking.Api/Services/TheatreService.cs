
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.DTOs.Theatres;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Services;

public class TheatreService : ITheatreService
{
    private readonly ITheatreRepository _repository;

    public TheatreService(ITheatreRepository repository)
    {
        _repository = repository;
    }

    public async Task<TheatreResponse> CreateAsync(
        CreateTheatreRequest request)
    {
        var theatre = new Theatre
        {
            Name = request.Name.Trim(),
            Location = request.Location.Trim()
        };

        var created = await _repository.CreateAsync(theatre);

        return MapToResponse(created);
    }

    public async Task<List<TheatreResponse>> GetAllAsync()
    {
        var theatres = await _repository.GetAllAsync();

        return theatres.Select(MapToResponse).ToList();
    }

    public async Task<TheatreResponse?> GetByIdAsync(int id)
    {
        var theatre = await _repository.GetByIdAsync(id);

        return theatre == null ? null : MapToResponse(theatre);
    }

    public async Task<TheatreResponse?> UpdateAsync(
        int id,
        UpdateTheatreRequest request)
    {
        var theatre = await _repository.GetByIdAsync(id);

        if (theatre == null)
            return null;

        theatre.Name = request.Name.Trim();
        theatre.Location = request.Location.Trim();

        var updated = await _repository.UpdateAsync(theatre);

        return MapToResponse(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var theatre = await _repository.GetByIdAsync(id);

        if (theatre == null)
            return false;

        try
        {
            return await _repository.DeleteAsync(theatre);
        }
        catch (DbUpdateException)
        {
            // Theatre has dependent screens or other records.
            return false;
        }
    }

    private static TheatreResponse MapToResponse(Theatre theatre)
    {
        return new TheatreResponse
        {
            TheatreId = theatre.TheatreId,
            Name = theatre.Name,
            Location = theatre.Location
        };
    }
}
