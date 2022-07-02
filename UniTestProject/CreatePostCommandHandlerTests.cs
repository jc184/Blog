using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using Blog.Core.Handlers.Commands;
using FluentValidation;
using MediatR;
using Moq;

namespace UnitTestProject
{
    public class CreatePostCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Int()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Posts.Add(It.IsAny<Post>()));
            var mockValidator = new Mock<IValidator<CreatePostDTO>>();


            var value = new FluentValidation.Results.ValidationResult();
            mockValidator.Setup(x => x.Validate(It.IsAny<CreatePostDTO>())).Returns(value);

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new CreatePostCommandHandler(mockRepo.Object, mockValidator.Object);
            var model = new CreatePostDTO() { Title = "Test", Body = "Test", FirstName = "TestName", LastName = "TestName", Email = "jc123@gmail.com", Status = "test" };
            var request = new CreatePostCommand(model);
            var requestModel = request.Model;

            var validModel = mockValidator.Object.Validate(model);
            var entity = new Post();
            mockRepo.Object.Posts.Add(entity);

            var result = await handler.Handle(new CreatePostCommand(requestModel), CancellationToken.None);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(0, result);
        }
    }
}
