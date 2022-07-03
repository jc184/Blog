using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.DTO;
using Blog.Core.Handlers.Queries;
using MediatR;
using Moq;

namespace UnitTestProject
{
    public class GetAllCommentsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_IEnumerableOfCommentDTOs()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Comments.GetAll());
            var mockMapper = new Mock<IMapper>();

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new GetAllCommentsQueryHandler(mockRepo.Object, mockMapper.Object);
            var model = new List<CommentDTO>();
            var request = new GetAllCommentsQuery();

            var result = await handler.Handle(new GetAllCommentsQuery(), CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<CommentDTO>>(result);
            Assert.Equal(model, result);
        }
    }
}
