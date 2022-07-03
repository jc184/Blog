using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Core.Handlers.Commands;
using MediatR;
using Moq;

namespace UnitTestProject
{
    public class DeletePostCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Int()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Posts.Delete(It.IsAny<Post>()));

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new DeletePostCommandHandler(mockRepo.Object);
            int id = 0;

            mockRepo.Object.Posts.Delete(id);

            var result = await handler.Handle(new DeletePostCommand(id), CancellationToken.None);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(0, result);
        }
    }
}
