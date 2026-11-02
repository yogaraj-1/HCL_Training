
using MovieTicketBooking.Api.Models;

namespace MovieTicketBooking.Api.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<Seat> CreateAsync(Seat seat);

    Task<List<Seat>> CreateBulkAsync(List<Seat> seats);

    Task<List<Seat>> GetByScreenIdAsync(int screenId);

    Task<Seat?> GetByIdAsync(int id);

    Task<bool> ScreenExistsAsync(int screenId);

    Task<bool> SeatExistsAsync(
        int screenId,
        string rowName,
        int seatNumber,
        int? excludeSeatId = null);

    Task<Seat> UpdateAsync(Seat seat);

    Task<bool> DeleteAsync(Seat seat);
}
