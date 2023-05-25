using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication.Data;
using WebApplicationLibrary.Data;
using WebApplicationLibrary.Models;

public class LibraryContextInitializer
{
    private readonly LibraryContext _context;

    public List<Genre> Genres { get; private set; }

    public LibraryContextInitializer(LibraryContext context)
    {
        _context = context;

        // Инициализация списка жанров 
        Genres = new List<Genre>
        {
            new Genre {Id = 1, Name = "Drama"},
            new Genre {Id = 2, Name = "Fiction"},
            new Genre {Id = 3, Name = "Science Fiction"},
            new Genre {Id = 4, Name = "Romance"},
            new Genre {Id = 5, Name = "Coming-of-Age"},
            new Genre {Id = 6, Name = "Gothic Fiction"},
            new Genre {Id = 7, Name = "Fantasy "},
            new Genre {Id = 8, Name = "Mystery "},
            new Genre {Id = 9, Name = "Historical Fiction"},
            new Genre {Id = 10, Name = "Adventure"},
            new Genre {Id = 11, Name = "Psychological Fiction"},
            new Genre {Id = 12, Name = "Magical Realism"}
        };
    }

    public async Task SeedAsync()
    {
        try
        {
            if (!await _context.Books.AnyAsync())
            {
                var books = new List<Book>
                {
                        new Book { Title = "The Great Gatsby", Author = new Author { FirstName = "F. Scott", LastName = "Fitzgerald" }, GenreId = 1 },
                        new Book { Title = "To Kill a Mockingbird", Author = new Author { FirstName = "Harper", LastName = "Lee" }, GenreId = 2 },
                        new Book { Title = "1984", Author = new Author { FirstName = "George", LastName = "Orwell" }, GenreId = 3 },
                        new Book { Title = "Pride and Prejudice", Author = new Author { FirstName = "Jane", LastName = "Austen" }, GenreId = 4 },
                        new Book { Title = "The Catcher in the Rye", Author = new Author { FirstName = "J.D.", LastName = "Salinger" }, GenreId = 5 },
                        new Book { Title = "Wuthering Heights", Author = new Author { FirstName = "Emily", LastName = "Bronte" }, GenreId = 6 },
                        new Book { Title = "The Lord of the Rings", Author = new Author { FirstName = "J.R.R.", LastName = "Tolkien" }, GenreId = 7 },
                        new Book { Title = "The Hobbit", Author = new Author { FirstName = "J.R.R.", LastName = "Tolkien" }, GenreId = 7 },
                        new Book { Title = "The Chronicles of Narnia", Author = new Author { FirstName = "C.S.", LastName = "Lewis" }, GenreId = 7 },
                        new Book { Title = "Frankenstein", Author = new Author { FirstName = "Mary", LastName = "Shelley" }, GenreId = 6 },
                        new Book { Title = "Dracula", Author = new Author { FirstName = "Bram", LastName = "Stoker" }, GenreId = 6 },
                        new Book { Title = "Jane Eyre", Author = new Author { FirstName = "Charlotte", LastName = "Bronte" }, GenreId = 4 },
                        new Book { Title = "The Picture of Dorian Gray", Author = new Author { FirstName = "Oscar", LastName = "Wilde" }, GenreId = 6 },
                        new Book { Title = "The Adventures of Sherlock Holmes", Author = new Author { FirstName = "Arthur Conan", LastName = "Doyle" }, GenreId = 8 },
                        new Book { Title = "Les Miserables", Author = new Author { FirstName = "Victor", LastName = "Hugo" }, GenreId = 9 },
                        new Book { Title = "The Three Musketeers", Author = new Author { FirstName = "Alexandre", LastName = "Dumas" }, GenreId = 10 },
                        new Book { Title = "War and Peace", Author = new Author { FirstName = "Leo", LastName = "Tolstoy" }, GenreId = 9 },
                        new Book { Title = "Crime and Punishment", Author = new Author { FirstName = "Fyodor", LastName = "Dostoevsky" }, GenreId = 11 },
                        new Book { Title = "The Brothers Karamazov", Author = new Author { FirstName = "Fyodor", LastName = "Dostoevsky" }, GenreId = 11 },
                        new Book { Title = "One Hundred Years of Solitude", Author = new Author { FirstName = "Gabriel Garcia", LastName = "Marquez" }, GenreId = 12 }

                };

               

                // добавляем эти данные в базу данных
                await _context.Books.AddRangeAsync(books);
                await _context.Genres.AddRangeAsync(Genres);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Ошибка при инициализации базы данных");

            // Можно выбросить кастомное исключение, чтобы обработать его на уровне выше
            throw new LibraryContextInitializerException("Ошибка при инициализации базы данных", ex);
        }
    }
}
