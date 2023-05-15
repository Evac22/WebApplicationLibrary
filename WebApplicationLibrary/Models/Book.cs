using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationLibrary.Models
{
    public class Book
    {
        //Идентификационные свойства
        public int Id { get; set; }//идентификатор книги
        public int AuthorId { get; set; }//идентификатор автора книги
        public int GenreId { get; set; }//идентификатор жанра книги
        public int PublisherId { get; set; }//идентификатор издателя книги

        //Основные свойства
        public string Title { get; set; }//название книги
        public DateTime PublicationDate { get; set; }//дата публикации книги
        public decimal Rating { get; set; }//рейтинг книги
        public int ReviewsNumber { get; set; }//количество отзывов о книге

        //Свойства-навигаторы
        public Author Author { get; set; }// автор книги
        public Genre Genre { get; set; }//жанр книги
        public Publisher Publisher { get; set; }// издатель книги

        //Свойства-навигаторы множественности     
        public ICollection<Reservation> Reservations { get; set; }// колцлекция бронирований книги
        public ICollection<Borrow> Borrows { get; set; }//коллекция аренд книги
    }
}
