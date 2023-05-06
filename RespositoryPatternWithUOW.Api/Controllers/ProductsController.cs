using Generic.UoW.Core;
using Generic.UoW.Core.Consts;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Products;
using RepositoryPatternWithUOW.EF;

namespace RespositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork<ProductContext> _unitOfWork;

        public ProductsController(IUnitOfWork<ProductContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Repository<Category>().GetById(1));
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Repository<Category>().GetAll());
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(_unitOfWork.Repository<Category>().Find(b => b.Name == "New Category", new[] { "Author" }));
        }

        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors()
        {
            return Ok(_unitOfWork.Repository<Category>().FindAll(b => b.Name.Contains("New Category"), new[] { "Author" }));
        }

        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(_unitOfWork.Repository<Category>().FindAll(b => b.Name.Contains("New Category"), null, null, b => b.Id, OrderBy.Descending));
        }

        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            var Category = _unitOfWork.Repository<Category>().Add(new Category { Name = "New Category", Id = 1 });
            _unitOfWork.Complete();
            return Ok(Category);
        }
    }
}