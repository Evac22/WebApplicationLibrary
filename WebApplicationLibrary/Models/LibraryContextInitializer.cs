

using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplicationLibrary.Data;
using WebApplication.Data;
using WebApplicationLibrary.Models;

//инициализирует начальные данные для базы данных LibraryContext
public class LibraryContextInitializer
{
    private readonly LibraryContext _context;

    public LibraryContextInitializer(LibraryContext context)
    {
        _context = context;
    }

    //Метод Seed() проверяет, есть ли уже какие-либо записи в таблице Books и жанров,
    //и если нет, то создает некоторые начальные данные. Это включает в себя список книг с их заголовками и авторами,
    //а также список жанров книг. 
    public async Task SeedAsync()
    {
        try
        {
            if (!await _context.Books.AnyAsync())
            {
                var books = new List<Book>
                {
                    new Book { Title = "The Great Gatsby", Author = new Author { FirstName = "F. Scott", LastName = "Fitzgerald" } },
                    new Book { Title = "To Kill a Mockingbird", Author = new Author { FirstName = "Harper", LastName = "Lee" } },
                    new Book { Title = "1984", Author = new Author { FirstName = "George", LastName = "Orwell" } },
                    new Book { Title = "Pride and Prejudice", Author = new Author { FirstName = "Jane", LastName = "Austen" } },
                    new Book { Title = "The Catcher in the Rye", Author = new Author { FirstName = "J.D.", LastName = "Salinger" } },
                    new Book { Title = "Wuthering Heights", Author = new Author { FirstName = "Emily", LastName = "Bronte" } },
                    new Book { Title = "The Lord of the Rings", Author = new Author { FirstName = "J.R.R.", LastName = "Tolkien" } },
                    new Book { Title = "The Hobbit", Author = new Author { FirstName = "J.R.R.", LastName = "Tolkien" } },
                    new Book { Title = "The Chronicles of Narnia", Author = new Author { FirstName = "C.S.", LastName = "Lewis" } },
                    new Book { Title = "Frankenstein", Author = new Author { FirstName = "Mary", LastName = "Shelley" } },
                    new Book { Title = "Dracula", Author = new Author { FirstName = "Bram", LastName = "Stoker" } },
                    new Book { Title = "Jane Eyre", Author = new Author { FirstName = "Charlotte", LastName = "Bronte" } },
                    new Book { Title = "The Picture of Dorian Gray", Author = new Author { FirstName = "Oscar", LastName = "Wilde" } },
                    new Book { Title = "The Adventures of Sherlock Holmes", Author = new Author { FirstName = "Arthur Conan", LastName = "Doyle" } },
                    new Book { Title = "Les Miserables", Author = new Author { FirstName = "Victor", LastName = "Hugo" } },
                    new Book { Title = "The Three Musketeers", Author = new Author { FirstName = "Alexandre", LastName = "Dumas" } },
                    new Book { Title = "War and Peace", Author = new Author { FirstName = "Leo", LastName = "Tolstoy" } },
                    new Book { Title = "Crime and Punishment", Author = new Author { FirstName = "Fyodor", LastName = "Dostoevsky" } },
                    new Book { Title = "The Brothers Karamazov", Author = new Author { FirstName = "Fyodor", LastName = "Dostoevsky" } },
                    new Book { Title = "One Hundred Years of Solitude", Author = new Author { FirstName = "Gabriel Garcia", LastName = "Marquez" } }

                };

                // добавляем эти данные в базу данных
                await _context.Books.AddRangeAsync(books);
                await _context.SaveChangesAsync();
            }

            if (!await _context.Genres.AnyAsync())
            {
                var genres = new List<Genre>
                {
                     new Genre { Name = "Classic" },
                     new Genre { Name = "Science Fiction" },
                     new Genre { Name = "Mystery" }
                };

                await _context.Genres.AddRangeAsync(genres);
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
