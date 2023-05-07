using AutoFixture;
using Generic.UoW.Core;
using Generic.UoW.Infra;
using Moq;
using RepositoryPatternWithUOW.Infra;

namespace RepositoryPatternWithUOW.Test.Controllers.Test
{
    public class AuthorsControllerTest
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly Mock<IUnitOfWork<ApplicationDbContext>> _unitOfWorkMock;
        private readonly Fixture _fixture;


        public AuthorsControllerTest(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _fixture = new Fixture();       
            _unitOfWorkMock = new Mock<IUnitOfWork<ApplicationDbContext>>();
            _unitOfWork = _unitOfWorkMock.Object;

        }
    }
}
