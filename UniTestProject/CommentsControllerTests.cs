using Blog.API.Controllers;
using Blog.Contracts.DTO;
using Blog.Core.Exceptions;
using Blog.Core.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTestProject
{
    public class CommentsControllerTests
    {
        [Fact]
        public async Task PostComment_returns_ObjectResult()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var model = new CreateCommentDTO() { Content = "Test", Email = "js123@gmail.com", FirstName = "John", LastName = "Smith", PostId = 1 };
            var result = await controller.Post(model);
            var okResult = result as ObjectResult;
            Assert.IsType<ObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(201, okResult?.StatusCode);
        }



        [Fact]
        public async Task Post_throws_exception_when_RequestBody_Invalid()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var model = new CreateCommentDTO() { Content = null, Email = null, FirstName = null, LastName = null, PostId = 0 };
            var result = await controller.Post(model);
            _ = Assert.ThrowsAsync<InvalidRequestBodyException>(async () => await controller.Post(model));
        }

        [Fact]
        public async Task GetAll_returns_OkResult()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var model = new List<CommentDTO>();
            var result = await controller.Get();
            var okResult = result as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public async Task GetById_Returns_OkResult()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var model = new CommentDTO();
            var result = await controller.GetById(1);
            var okResult = result as OkObjectResult;
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public async Task GetById_throws_exception_when_EntityNotFound()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var result = await controller.GetById(1);
            _ = Assert.ThrowsAsync<EntityNotFoundException>(async () => await controller.GetById(1));
        }

        [Fact]
        public async Task Delete_Returns_ObjectResult()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var model = new CommentDTO();
            var result = await controller.Delete(1);
            var okResult = result as ObjectResult;
            Assert.IsType<ObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public async Task Delete_throws_exception_when_RequestBody_Invalid()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            int id = 0;
            var result = await controller.Delete(id);
            _ = Assert.ThrowsAsync<InvalidRequestBodyException>(async () => await controller.Delete(id));
        }

        [Fact]
        public async Task Update_Returns_ObjectResult()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var model = new UpdateCommentDTO();
            int id = 1;
            var result = await controller.Update(model, id);
            var okResult = result as ObjectResult;
            Assert.IsType<ObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(200, okResult?.StatusCode);
        }

        [Fact]
        public async Task Update_throws_exception_when_RequestBody_Invalid()
        {
            var mediator = new Mock<IMediator>();
            var controller = new CommentsController(mediator.Object);
            var model = new UpdateCommentDTO() { Content = null, Email = null, FirstName = null, LastName = null, PostId = 0 };
            int id = 1;
            var result = await controller.Update(model, id);
            _ = Assert.ThrowsAsync<InvalidRequestBodyException>(async () => await controller.Update(model, id));
        }
            
        [Fact]
        public void Post_SendsQueryWithTheCorrectData()
        {

            var model = new CreateCommentDTO() { Content = "Test", Email = "js123@gmail.com", FirstName = "John", LastName = "Smith", PostId = 1 };
            var mediator = new Mock<IMediator>();
            var sut = new CommentsController(mediator.Object);

            sut?.Post(model);

            mediator.Verify(x => x.Send(It.Is<CreateCommentCommand>(y => y.Model.Content == model.Content), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(x => x.Send(It.Is<CreateCommentCommand>(y => y.Model.FirstName == model.FirstName), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(x => x.Send(It.Is<CreateCommentCommand>(y => y.Model.LastName == model.LastName), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(x => x.Send(It.Is<CreateCommentCommand>(y => y.Model.Email == model.Email), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(x => x.Send(It.Is<CreateCommentCommand>(y => y.Model.PostId == model.PostId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}