using Microsoft.EntityFrameworkCore;
using WebApplicationLibrary.Models;


namespace WebApplication.Data
{
    public class LibraryContext : DbContext // контекст базы данных, который используем для взаимодействия с базой данных
    {
        //конструктор, который принимает параметры опций контекста
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        // Определяем наборы объектов, которые будут представлены таблицами в базе данных
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        // Определяем метод, который настраивает отношения между таблицами в базе данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определяем отношение один-ко-многим между таблицами Book и Author
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author);// Каждая книга имеет одного автора
       
           
            // Определяем отношение один-ко-многим между таблицами Borrow и Book
            modelBuilder.Entity<Borrow>()
                .HasOne(b => b.Book)// Каждая аренда связана с одной книгой
                .WithMany(b => b.Borrows)// У каждой книги может быть несколько аренд
                .HasForeignKey(b => b.BookId);// В качестве внешнего ключа используется свойство BookId

            // Определяем отношение один-ко-многим между таблицами Book и Publisher
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)//у каждой книги есть один издатель
                .WithMany(b => b.Books)//у каждого издателя может быть множество книг, связанных с этим издателем,
                .HasForeignKey(b => b.PublisherId);

            // Определяем отношение один-ко-многим между таблицами Book и Genre
            modelBuilder.Entity<Book>()
                 .HasOne(b => b.Genre)//Каждая книга имеет один жанр
                 .WithMany(g => g.Books)// каждый жанр может содержать множество книг
                 .HasForeignKey(b => b.GenreId);

            // Настраиваем отношение "один-ко-многим" между сущностями Book и Reservation
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reservations)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId);

            // Настраиваем отношение "один-ко-многим" между сущностями Book и Borrow
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Borrows)//каждая книга может иметь много бронирований
                .WithOne(br => br.Book)// каждое бронирование должно относиться к одной книге 
                .HasForeignKey(br => br.BookId);

            // Настраиваем связь между сущностями Reservation и Book
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reservations)// Один экземпляр книги может быть забронирован несколькими пользователями
                .HasForeignKey(r => r.BookId);// в качестве внешнего ключа используется свойство BookId

            // Настраиваем связь между сущностями User и Borrow
            modelBuilder.Entity<User>()
                .HasMany(u => u.Borrows) // У пользователя может быть несколько аренд
                .WithOne(b => b.User)// У каждой аренды должен быть связанный пользователь
                .HasForeignKey(b => b.UserId);// в качестве внешнего ключа используется свойство UserId

            // Настраиваем связь между сущностями User и Reservation
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations) // У пользователя может быть несколько бронирований
                .WithOne(r => r.User)// У каждого бронирования должен быть связанный пользователь
                .HasForeignKey(r => r.UserId); // в качестве внешнего ключа используется свойство UserId
        }

    }
}
