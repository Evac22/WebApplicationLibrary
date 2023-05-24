using WebApplicationLibrary.Models;

namespace WebApplicationLibrary.Repository
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAllGenres();
        Genre GetGenreById(int id);
        void AddGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int id);
    }

    public class GenreRepository : IGenreRepository
    {
        private List<Genre> _genres;

        public GenreRepository()
        {
            _genres = new List<Genre>();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _genres;
        }

        public Genre GetGenreById(int id)
        {
            return _genres.Find(g => g.Id == id);
        }

        public void AddGenre(Genre genre)
        {
            _genres.Add(genre);
        }

        public void UpdateGenre(Genre genre)
        {
            Genre existingGenre = _genres.Find(g => g.Id == genre.Id);
            if (existingGenre != null)
            {
                existingGenre.Name = genre.Name;
            }
        }

        public void DeleteGenre(int id)
        {
            Genre genreToRemove = _genres.Find(g => g.Id == id);
            if (genreToRemove != null)
            {
                _genres.Remove(genreToRemove);
            }
        }
    }
}
