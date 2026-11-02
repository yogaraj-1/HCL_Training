
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Api.DTOs.Movies;
using MovieTicketBooking.Api.Models;
using MovieTicketBooking.Api.Repositories.Interfaces;
using MovieTicketBooking.Api.Services.Interfaces;

namespace MovieTicketBooking.Api.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _repository;

    public MovieService(IMovieRepository repository)
    {
        _repository = repository;
    }

    // ENTITY TO RESPONSE DTO
    private static MovieResponse MapToResponse(Movie movie)
    {
        return new MovieResponse
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Description = movie.Description,
            DurationMinutes = movie.DurationMinutes,
            ReleaseDate = movie.ReleaseDate,
            Language = movie.Language,
            Genre = movie.Genre
        };
    }

    // CREATE
    public async Task<MovieResponse> CreateAsync(
        CreateMovieRequest request)
    {
        var movie = new Movie
        {
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            DurationMinutes = request.DurationMinutes,
            ReleaseDate = request.ReleaseDate,
            Language = request.Language.Trim(),
            Genre = request.Genre.Trim()
        };

        var createdMovie = await _repository.CreateAsync(movie);

        return MapToResponse(createdMovie);
    }

    // GET BY ID
    public async Task<MovieResponse?> GetByIdAsync(int id)
    {
        var movie = await _repository.GetByIdAsync(id);

        if (movie == null)
            return null;

        return MapToResponse(movie);
    }

    // GET ALL + FILTER + PAGINATION
    public async Task<PagedResponse<MovieResponse>> GetAllAsync(
        MovieQueryParameters parameters)
    {
        parameters.PageNumber = Math.Max(1, parameters.PageNumber);

        parameters.PageSize = Math.Clamp(
            parameters.PageSize, 1, 100);

        var result = await _repository.GetAllAsync(parameters);

        return new PagedResponse<MovieResponse>
        {
            Items = result.Items.Select(MapToResponse),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords,
            TotalPages = result.TotalPages
        };
    }

    // UPDATE
    public async Task<MovieResponse?> UpdateAsync(
        int id,
        UpdateMovieRequest request)
    {
        var movie = await _repository.GetByIdAsync(id);

        if (movie == null)
            return null;

        movie.Title = request.Title.Trim();
        movie.Description = request.Description?.Trim();
        movie.DurationMinutes = request.DurationMinutes;
        movie.ReleaseDate = request.ReleaseDate;
        movie.Language = request.Language.Trim();
        movie.Genre = request.Genre.Trim();

        var updatedMovie = await _repository.UpdateAsync(movie);

        return MapToResponse(updatedMovie);
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _repository.GetByIdAsync(id);

        if (movie == null)
            return false;

        try
        {
            await _repository.DeleteAsync(movie);
            return true;
        }
        catch (DbUpdateException)
        {
            // Movie has dependent show records.
            return false;
        }
    }

    // SEARCH
    public async Task<IEnumerable<MovieResponse>> SearchByTitleAsync(
        string title)
    {
        var movies = await _repository.SearchByTitleAsync(title.Trim());

        return movies.Select(MapToResponse);
    }
}
