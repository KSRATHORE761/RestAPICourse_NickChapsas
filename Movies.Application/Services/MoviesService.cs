using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IValidator<Movie> _movievalidator;  
        public MoviesService(IMovieRepository movieRepository, IValidator<Movie> movievalidator)
        {
            _movieRepository = movieRepository;
            _movievalidator = movievalidator;
        }
        public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
        {
            await _movievalidator.ValidateAndThrowAsync(movie, token);
            return await _movieRepository.CreateAsync(movie, token); 
        }

        public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            return _movieRepository.DeleteByIdAsync(id, token);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token=default)
        {
           return await _movieRepository.GetAllAsync(token);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
           return await _movieRepository.GetByIdAsync(id, token);  
        }

        public async Task<Movie?> GetBySlug(string slug, CancellationToken token = default)
        {
            return await _movieRepository.GetBySlug(slug, token);
        }

        public async Task<Movie?> UpdateAsync(Movie movie,CancellationToken token= default)
        {
            await _movievalidator.ValidateAndThrowAsync(movie,token);
            await _movieRepository.UpdateAsync(movie,token);
            return movie;
        }
    }
}
