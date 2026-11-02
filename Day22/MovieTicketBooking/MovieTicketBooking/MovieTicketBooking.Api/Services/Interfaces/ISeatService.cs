
using MovieTicketBooking.Api.DTOs.Seats;

namespace MovieTicketBooking.Api.Services.Interfaces;

public interface ISeatService
{
    Task<SeatResponse?> CreateAsync(CreateSeatRequest request);

    Task<List<SeatResponse>?> CreateBulkAsync(
        BulkCreateSeatsRequest request);

    Task<List<SeatResponse>?> GetByScreenIdAsync(int screenId);

    Task<SeatResponse?> GetByIdAsync(int id);

    Task<SeatResponse?> UpdateAsync(
        int id,
        UpdateSeatRequest request);

    Task<bool> DeleteAsync(int id);
}
