using WebApplicationLibrary.Models;

namespace WebApplicationLibrary.DTO
{
    //Класс BookDto (Data Transfer Object) используется для передачи данных веб-приложением между клиентом и сервером
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public decimal Rating { get; set; }
        public int ReviewsNumber { get; set; }
    }

}