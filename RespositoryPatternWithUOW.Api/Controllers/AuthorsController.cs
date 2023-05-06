using Generic.UoW.Core;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF;
using System.Threading.Tasks;

namespace RespositoryPatternWithUOW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

        public AuthorsController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_unitOfWork.Repository<Author>().GetById(1));
        }

        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetByIdAsync()
        {
            return Ok(await _unitOfWork.Repository<Author>().GetByIdAsync(1));
        }
    }
}