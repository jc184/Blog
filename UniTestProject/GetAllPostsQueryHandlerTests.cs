using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.DTO;
using Blog.Core.Handlers.Queries;
using MediatR;
using Moq;

namespace UnitTestProject
{
    public class GetAllPostsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_IEnumerableOfPostDTOs()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Posts.GetAll());
            var mockMapper = new Mock<IMapper>();

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new GetAllPostsQueryHandler(mockRepo.Object, mockMapper.Object);
            var model = new List<PostDTO>();
            var request = new GetAllPostsQuery();

            var result = await handler.Handle(new GetAllPostsQuery(), CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<PostDTO>>(result);
            Assert.Equal(model, result);
        }
    }
}
