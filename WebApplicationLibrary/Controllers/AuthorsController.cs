using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Repositories;

namespace WebApplicationLibrary.Controllers
{
    // Этот атрибут указывает на то, что путь URL-адреса для методов в этом контроллере будет начинаться с /api/authors
    [Route("api/[controller]")]
    // Атрибут указывает на то, что это контроллер веб-API и обрабатывает запросы RESTful.
    [ApiController]
    public class AuthorsController : Controller
    {
        //Интерфейс IAuthorRepository определяет контракты для работы с хранилищем данных для авторов, и это поле используется в контроллере для взаимодействия с этим хранилищем данных
        private readonly IAuthorRepository _authorRepository;
        
        // Конструктор
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public IEnumerable<Author> GetAuthors()
        {
            return _authorRepository.GetAll();
        }

        [HttpGet("{id}")]
        //Возвращаю список всех авторов из репозитория
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _authorRepository.GetById(id);

            if(author == null)
            {
                return NotFound();
            }

            return author;
        }

        [HttpPost]
        //Метод CreateAuthor принимает объект автора и добавляет его в репозиторий.
        //Затем метод возвращает объект CreatedAtAction, который возвращает статус код 201 (создано)
        //и URL-адрес, который можно использовать для получения созданного объекта.
        public ActionResult<Author> CreateAuthor(Author author)
        {
            _authorRepository.Add(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        //Метод UpdateAuthor принимает параметр id и объект автора, который содержит обновленные данные для автора с заданным идентификатором.
        //Если идентификаторы не совпадают, метод возвращает статус код 400 (неверный запрос).
        //В противном случае, метод обновляет данные автора в репозитории и возвращает статус код 204 (нет содержимого).
        public IActionResult UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _authorRepository.Update(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        //Метод DeleteAuthor принимает параметр id и удаляет автора с заданным идентификатором из репозитория.
        //Если автор не найден, метод возвращает статус код 404 (не найдено).
        //В противном случае, метод возвращает статус код 204 (нет содержимого).
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authorRepository.GetById(id);

            if (author == null)
            {
                return NotFound();
            }

            _authorRepository.Delete(id);

            return NoContent();
        }
    }
}
