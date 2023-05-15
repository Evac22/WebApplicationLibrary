
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
            if(!await _context.Books.AnyAsync())
            {
                var books = new List<Book>
                {
                    new Book { Title = "The Great Gatsby", Author = new Author { Name = "F. Scott Fitzgerald" } },
                    new Book { Title = "To Kill a Mockingbird", Author = new Author { Name = "Harper Lee" } },
                    new Book { Title = "1984", Author = new Author { Name = "George Orwell" } },
                    new Book { Title = "Pride and Prejudice", Author = new Author { Name = "Jane Austen" } },
                    new Book { Title = "The Catcher in the Rye", Author = new Author { Name = "J.D. Salinger" } },
                    new Book { Title = "Wuthering Heights", Author = new Author { Name = "Emily Bronte" } },
                    new Book { Title = "The Lord of the Rings", Author = new Author { Name = "J.R.R. Tolkien" } },
                    new Book { Title = "The Hobbit", Author = new Author { Name = "J.R.R. Tolkien" } },
                    new Book { Title = "The Chronicles of Narnia", Author = new Author { Name = "C.S. Lewis" } },
                    new Book { Title = "Frankenstein", Author = new Author { Name = "Mary Shelley" } },
                    new Book { Title = "Dracula", Author = new Author { Name = "Bram Stoker" } },
                    new Book { Title = "Jane Eyre", Author = new Author { Name = "Charlotte Bronte" } },
                    new Book { Title = "The Picture of Dorian Gray", Author = new Author { Name = "Oscar Wilde" } },
                    new Book { Title = "The Adventures of Sherlock Holmes", Author = new Author { Name = "Arthur Conan Doyle" } },
                    new Book { Title = "Les Miserables", Author = new Author { Name = "Victor Hugo" } },
                    new Book { Title = "The Three Musketeers", Author = new Author { Name = "Alexandre Dumas" } },
                    new Book { Title = "War and Peace", Author = new Author { Name = "Leo Tolstoy" } },
                    new Book { Title = "Crime and Punishment", Author = new Author { Name = "Fyodor Dostoevsky" } },
                    new Book { Title = "The Brothers Karamazov", Author = new Author { Name = "Fyodor Dostoevsky" } },
                    new Book { Title = "One Hundred Years of Solitude", Author = new Author { Name = "Gabriel Garcia Marquez" } }
                };

                // добавляем эти данные в базу данных
                await _context.Books.AddRangeAsync(books);
                await _context.SaveChangesAsync();
            }

            if(!await _context.Genres.AnyAsync())
            {
                var genres = new List<Genre>
                {
                     new Genre { GenreName = "Classic" },
                     new Genre { GenreName = "Science Fiction" },
                     new Genre { GenreName = "Mystery" }
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
