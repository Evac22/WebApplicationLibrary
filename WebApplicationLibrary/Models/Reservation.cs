namespace WebApplicationLibrary.Models
{//Класс, представляющий отношение между книгой и пользователем, когда пользователь резервирует книгу в библиотеке. Включает свойства, такие как дата резервирования, дата отмены и т.д.
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservedDate { get; set; }//дата бронирования
        public bool IsAvailable { get; set; }//флаг, указывающий, доступна ли книга для бронирования
        public User User { get; set; }//ссылка на пользователя, который забронировал книгу
        public Book Book { get; set; }// ссылка на книгу, которая была забронирована пользователем
    }
}
