using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Core.Handlers.Commands;
using MediatR;
using Moq;

namespace UnitTestProject
{
    public class DeleteCommentCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Int()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Comments.Delete(It.IsAny<Comment>()));

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new DeleteCommentCommandHandler(mockRepo.Object);
            int id = 0;
            
            mockRepo.Object.Comments.Delete(id);

            var result = await handler.Handle(new DeleteCommentCommand(id), CancellationToken.None);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(0, result);
        }
    }
}
