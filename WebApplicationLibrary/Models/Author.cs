namespace WebApplicationLibrary.Models
{   //: Класс, представляющий автора книги. Включает свойства, такие как имя, фамилия, дата рождения и т.д.
    public class Author
    {
        public int Id { get; set; }// уникальный идентификатор автора
     
        public string FirstName { get; set; }//имя автора, если разделено на составляющие
        public string LastName { get; set; }//фамилия автора
        public List<Book> Books { get; set; }//список книг, написанных этим автором.
                                             //Это свойство представляет отношение один-ко-многим между автором и его книгами, так как один автор может иметь несколько книг
    }
}
