using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaWeb.Models.ViewModel
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genres { get; set; }
    }

    public static class MovieMapper
    {
        public static MovieDTO MapToDTO(Movie movie)
        {
            return new MovieDTO
            {
                Id = movie.id,
                Name = movie.name,
                Genres = string.Join(", ", movie.MovieGenres.Select(s => s.Genre.name))
            };
        }

        public static List<MovieDTO> MapToDTOList(List<Movie> movies)
        {
            return movies.Select(movie => MapToDTO(movie)).ToList();
        }
    }
}