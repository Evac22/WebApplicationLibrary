namespace WebApplicationLibrary.Models
{  //Класс, представляющий издательство книги. Включает свойства, такие как название, адрес и т.д.
    public class Publisher
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }// наименование издателя
        public string Address { get; set; }//адрес издателя
        public string Email { get; set; }// адрес электронной почты издателя   
        public string Phone { get; set; }// контактный телефон издателя
        public ICollection<Book> Books { get; set; }// коллекция книг, которые были опубликованы издательством
    }
}
