namespace WebApplicationLibrary.Models
{ //Класс, представляющий пользователя, который может брать книги в библиотеке. Включает свойства, такие как имя, фамилия, адрес электронной почты, пароль и т.д.
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }//пользователь с административными правами
        public ICollection<Borrow> Borrows { get; set; }//взятые на прокат
        public ICollection<Reservation> Reservations { get; set; }//забронированные  книги
    }
}
