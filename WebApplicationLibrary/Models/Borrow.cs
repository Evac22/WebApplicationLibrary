namespace WebApplicationLibrary.Models
{//Класс, представляющий отношение между книгой и пользователем, когда пользователь берет книгу в библиотеке. Включает свойства, такие как дата взятия, дата возврата и т.д.
    public class Borrow
    {
        //Идентификаторы книги и пользователя
        public int Id { get; set; }
        public int BookId { get; set; }// идентификатор книги, которую было взято
        public int UserId { get; set; }//идентификатор пользователя, который взял книгу

        //Ссылки на объекты Book и User:
        public Book Book { get; set; }//связывания экземпляра объекта Borrow с экземпляром объекта Book
        public User User { get; set; }//связь с объектом пользователя, который взял книгу в библиотеке

        //Даты
        public DateTime ReturnDate { get; set; }//???   
        public DateTime BorrowDate { get; set; }//дата, когда книга была взята.
        public DateTime DueDate { get; set; }//дата, когда книга должна быть возвращена.
        public DateTime? ReturnedDate { get; set; }//дата, когда книга была фактически возвращена.
    }
}
