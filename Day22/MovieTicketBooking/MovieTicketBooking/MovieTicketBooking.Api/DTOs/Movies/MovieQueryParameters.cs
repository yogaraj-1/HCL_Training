
namespace MovieTicketBooking.Api.DTOs.Movies;

public class MovieQueryParameters
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 5;

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public string? Language { get; set; }

    public string? SortBy { get; set; } = "MovieId";

    public string? SortOrder { get; set; } = "asc";
}
