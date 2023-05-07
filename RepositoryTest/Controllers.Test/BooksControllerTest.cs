using AutoFixture;
using FluentAssertions;
using Generic.UoW.Core;
using Moq;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Infra;
using RespositoryPatternWithUOW.Api.Controllers;

namespace RepositoryPatternWithUOW.Test.Controllers.Test
{
    public class BooksControllerTest
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly Mock<IUnitOfWork<ApplicationDbContext>> _unitOfWorkMock;
        private readonly Fixture _fixture;

        public BooksControllerTest()
        {
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork<ApplicationDbContext>>();
            _unitOfWork = _unitOfWorkMock.Object;

        }
        #region GetAll

        [Fact]
        public void GetAll_Should_return_all_books()
        {
            //Arrange
            List<Book> _books = _fixture.Create<List<Book>>();
            BooksController booksController = new BooksController(_unitOfWork);
            _unitOfWorkMock.Setup(temp => temp.Repository<Book>().GetAll()).Returns(_books);
            //Act
            var books = booksController.GetAll();
            //Assert
            books.Should().BeEquivalentTo(_books);

        }
        #endregion
    }
}
