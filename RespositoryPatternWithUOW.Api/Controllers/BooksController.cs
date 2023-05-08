using Generic.UoW.Core;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace RespositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Repository<Book>().GetById(1));
        }

        [HttpGet("GetAll")]
        public List<Book> GetAll()
        {
            var books=_unitOfWork.Repository<Book>().GetAll().ToList();
            return books;
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_unitOfWork.Repository<Book>().Find(b => b.Title == "New Book", new[] { "Author" }));
        }

        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors()
        {
            return Ok(_unitOfWork.Repository<Book>().FindAll(b => b.Title.Contains("New Book"), new[] { "Author" }));
        }

        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_unitOfWork.Repository<Book>().FindAll(b => b.Title.Contains("New Book"), null, null, b => b.Id, OrderBy.Descending));
        }

        [HttpPost("AddOne")]
        public IActionResult AddOne(Book dto)
        {
            var book = _unitOfWork.Repository<Book>().Add(dto);
            _unitOfWork.Complete();
            return Ok(book);
        }
    }
}