
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.DTOs.Screens;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Services;

public class ScreenService : IScreenService
{
    private readonly IScreenRepository _repository;

    public ScreenService(IScreenRepository repository)
    {
        _repository = repository;
    }

    public async Task<ScreenResponse?> CreateAsync(
        CreateScreenRequest request)
    {
        var theatreExists = await _repository
            .TheatreExistsAsync(request.TheatreId);

        if (!theatreExists)
            return null;

        var screen = new Screen
        {
            TheatreId = request.TheatreId,
            ScreenName = request.ScreenName.Trim()
        };

        var created = await _repository.CreateAsync(screen);

        return MapToResponse(created);
    }

    public async Task<List<ScreenResponse>> GetAllAsync(
        int? theatreId = null)
    {
        var screens = await _repository.GetAllAsync(theatreId);

        return screens.Select(MapToResponse).ToList();
    }

    public async Task<ScreenResponse?> GetByIdAsync(int id)
    {
        var screen = await _repository.GetByIdAsync(id);

        return screen == null ? null : MapToResponse(screen);
    }

    public async Task<List<ScreenResponse>?> GetByTheatreIdAsync(
        int theatreId)
    {
        var theatreExists = await _repository
            .TheatreExistsAsync(theatreId);

        if (!theatreExists)
            return null;

        var screens = await _repository.GetByTheatreIdAsync(theatreId);

        return screens.Select(MapToResponse).ToList();
    }

    public async Task<ScreenResponse?> UpdateAsync(
        int id,
        UpdateScreenRequest request)
    {
        var screen = await _repository.GetByIdAsync(id);

        if (screen == null)
            return null;

        var theatreExists = await _repository
            .TheatreExistsAsync(request.TheatreId);

        if (!theatreExists)
            return null;

        screen.TheatreId = request.TheatreId;
        screen.ScreenName = request.ScreenName.Trim();

        var updated = await _repository.UpdateAsync(screen);

        return MapToResponse(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var screen = await _repository.GetByIdAsync(id);

        if (screen == null)
            return false;

        try
        {
            return await _repository.DeleteAsync(screen);
        }
        catch (DbUpdateException)
        {
            // Screen has dependent seats or shows.
            return false;
        }
    }

    private static ScreenResponse MapToResponse(Screen screen)
    {
        return new ScreenResponse
        {
            ScreenId = screen.ScreenId,
            TheatreId = screen.TheatreId,
            ScreenName = screen.ScreenName
        };
    }
}
