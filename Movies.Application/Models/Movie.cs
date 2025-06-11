using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Application.Models
{
    public class Movie
    {
        public required Guid Id { get; init;}
        public required string Title { get; init;}
        public string Slug => GenerateSlug();
        public required int YearOfRelease { get; init;}
        public required List<string> Genres { get; init; } = new();
        private string GenerateSlug()
        {
            var slugedTittle = Regex.Replace(Title, "[^0-9A-Za-z _-]", string.Empty)
                .ToLower().Replace(" ", "-");
            //var slugedTittle = SlugRegex().Replace(Title,string.Empty).ToLower().Replace(" ", "-");
            return $"{slugedTittle}-{YearOfRelease}";
        }

        //[GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
        // private static partial Regex SlugRegex();
    }
}
