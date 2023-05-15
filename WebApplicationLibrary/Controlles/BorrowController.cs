using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Repositories;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowRepository _borrowRepository;

        public BorrowController(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        // GET: api/borrow
        [HttpGet]
        public IActionResult GetAll()
        {
            var borrows = _borrowRepository.GetAll();
            if (!borrows.Any())
            {
                return NoContent();
            }

            return Ok(borrows);
        }

        // GET: api/borrow/5
        [HttpGet("{id}", Name = "GetBorrowById")]
        public IActionResult GetById(int id)
        {
            var borrow = _borrowRepository.GetById(id);
            if (borrow == null)
            {
                return NotFound();
            }

            return Ok(borrow);
        }

        // POST: api/borrow
        [HttpPost]
        public IActionResult Add([FromBody] Borrow borrow)
        {
            if (borrow == null)
            {
                return BadRequest();
            }

            _borrowRepository.Add(borrow);

            return CreatedAtRoute("GetBorrowById", new { id = borrow.Id }, borrow);
        }

        // PUT: api/borrow/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Borrow borrow)
        {
            if (borrow == null || borrow.Id != id)
            {
                return BadRequest();
            }

            var existingBorrow = _borrowRepository.GetById(id);
            if (existingBorrow == null)
            {
                return NotFound();
            }

            existingBorrow.UserId = borrow.UserId;
            existingBorrow.BookId = borrow.BookId;
            existingBorrow.BorrowDate = borrow.BorrowDate;
            existingBorrow.DueDate = borrow.DueDate;
            existingBorrow.ReturnedDate = borrow.ReturnedDate;

            _borrowRepository.Update(existingBorrow);

            return new NoContentResult();
        }

        // DELETE: api/borrow/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var borrow = _borrowRepository.GetById(id);
            if (borrow == null)
            {
                return NotFound();
            }

            _borrowRepository.Delete(id);

            return new NoContentResult();
        }

        // GET: api/borrow/overdue
        [HttpGet("overdue")]
        public IActionResult GetOverdue()
        {
            var borrows = _borrowRepository.GetOverdue(DateTime.Now);
            if (!borrows.Any())
            {
                return NoContent();
            }

            return Ok(borrows);
        }
    }
}
