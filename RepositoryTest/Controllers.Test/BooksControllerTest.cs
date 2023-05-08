using AutoFixture;
using FluentAssertions;
using Generic.UoW.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.Infra;
using RespositoryPatternWithUOW.Api.Controllers;

namespace RepositoryPatternWithUOW.Test.Controllers.Test
{
    public class BooksControllerTest
    {
       // private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly Mock<IUnitOfWork<ApplicationDbContext>> _unitOfWorkMock;
        private readonly Fixture _fixture;

        public BooksControllerTest()
        {
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork<ApplicationDbContext>>();
           // _unitOfWork = _unitOfWorkMock.Object;

        }
        #region GetAll

        [Fact]
        public void GetAll_Should_return_all_books()
        {
            //Arrange
            List<Book> expectedItems = _fixture.Create<List<Book>>();
            BooksController booksController = new BooksController(_unitOfWorkMock.Object);
            _unitOfWorkMock.Setup(temp => temp.Repository<Book>().GetAll()).Returns(expectedItems);
            //Act
            var actualItems = booksController.GetAll();
            //Assert
            actualItems.Should().BeEquivalentTo(expectedItems);

        }
        #endregion

        #region AddOne

        [Fact]
        public void AddOne_item_null_Should_return_exception()
        {
            //Arrange
            Book dto = null;
            BooksController booksController = new BooksController(_unitOfWorkMock.Object);
            _unitOfWorkMock.Setup(temp => temp.Repository<Book>().Add(dto)).Returns(dto);
            //Act
            var result = booksController.AddOne(dto) as OkObjectResult;
            //Assert
          

        }
        [Fact]
        public void AddOne_Should_return_book()
        {
            //Arrange
            Book itemToCreate = _fixture.Create<Book>() ;
            BooksController booksController = new BooksController(_unitOfWorkMock.Object);
            _unitOfWorkMock.Setup(temp => temp.Repository<Book>().Add(itemToCreate)).Returns(itemToCreate);
            //Act
            var result = booksController.AddOne(itemToCreate) as OkObjectResult;
            //Assert
           // var createdItem=result?.Value as Book;
           // Assert.IsType<Book>(result?.Value);
            result.Value.Should().BeOfType< Book>();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be(itemToCreate);

        }
        #endregion
    }
}
