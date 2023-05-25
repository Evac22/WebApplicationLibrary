using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplicationLibrary.Models
{ //Класс, представляющий жанр книги. Включает свойства, такие как название, описание и т.д.
    public class Genre
    {
        public int Id { get; set; }//идентификатор жанра книги     
       
        public string Name { get; set; } //название жанра книги

        ////атрибут указывает, что свойство Books является обратной ссылкой на навигационное свойство Genre класса Book.
        ////Таким образом, при запросе списка книг, относящихся к определенному жанру, можно использовать навигационное свойство Books класса Genre.
        //[InverseProperty("Genre")]
        //public ICollection<Book> Books { get; set; }// коллекция книг, относящихся к данному жанру
    }
}
