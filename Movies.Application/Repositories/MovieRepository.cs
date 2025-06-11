using Microsoft.EntityFrameworkCore;
using Movies.Application.Database;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        //private readonly List<Movie> _movies = new();
        private readonly AppDbContext _dbContext;
        public MovieRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Movie movie, CancellationToken token = default)
        {
            movie.Slug = GenerateSlug(movie.Title, movie.YearOfRelease);
            await _dbContext.Movies.AddAsync(movie,token);
            var created  = await _dbContext.SaveChangesAsync(token);
            return created > 0;
        }
        public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
        {
            return await _dbContext.Movies.ToListAsync(token);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _dbContext.Movies.FindAsync(id, token);
        }

        public Task<Movie> GetBySlug(string slug,CancellationToken token)
        {
           return _dbContext.Movies.FirstOrDefaultAsync(x => x.Slug == slug,token);
        }
        public async Task<bool> UpdateAsync(Movie movie, CancellationToken token = default)
        {
            var exisitngMovie = await _dbContext.Movies.FindAsync(movie.Id, token);
            if (exisitngMovie is null)
            {
                return false;
            }
            exisitngMovie.Title = movie.Title;
            exisitngMovie.YearOfRelease = movie.YearOfRelease;
            exisitngMovie.Genres = movie.Genres;
            exisitngMovie.Slug = GenerateSlug(exisitngMovie.Title, exisitngMovie.YearOfRelease);
            var updated = await _dbContext.SaveChangesAsync(token);
            return updated > 0; 
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
        {
            var movie =await GetByIdAsync(id, token);
            if (movie == null)
            {
                return false;
            }
            _dbContext.Movies.Remove(movie);
            var deleted = await _dbContext.SaveChangesAsync(token);
            return deleted > 0;
        }
        private string GenerateSlug(string title, int yearOfRelease)
        {
            var slugedTitle = Regex.Replace(title, "[^0-9A-Za-z _-]", string.Empty)
                .ToLower().Replace(" ", "-");
            return $"{slugedTitle}-{yearOfRelease}";
        }
    }
}
