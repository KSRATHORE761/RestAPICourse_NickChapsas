using Movies.Application.Models;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.API.Mapping
{
    public static class ContractMapping
    {
        public static Movies.Application.Models.Movie MapToMovie(this CreateMovieRequest request)
        {
            return new Movies.Application.Models.Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList(),

            };
        }

        public static MovieResponse MapToResponse(this Movies.Application.Models.Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                Genres = movie.Genres
            };
        }

        public static MoviesResponse MapToResponse(this IEnumerable<Movies.Application.Models.Movie> movies)
        {
            return new MoviesResponse
            {
                Items = (IEnumerable<MoviesResponse>)movies.Select(MapToResponse)
            };
        }
        public static Movies.Application.Models.Movie MapToMovie(this UpdateMovieRequest request, Guid id)
        {
            return new Movies.Application.Models.Movie
            {
                Id = id,
                Title = request.Title,
                YearOfRelease = request.YearOfRelease,
                Genres = request.Genres.ToList(),

            };
        }
    }
}
