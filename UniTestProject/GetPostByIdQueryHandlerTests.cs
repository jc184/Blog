using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using Blog.Core.Handlers.Queries;
using MediatR;
using Moq;

namespace UnitTestProject
{
    public class GetPostByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_PostDTO()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            int id = 1;
            mockRepo.Setup(x => x.Posts.GetByIdInclude(id)).Returns(new Post() { Id = 1, Title = "Test", Body = "Test", FirstName = "TestName", LastName = "TestName", Email = "jc123@gmail.com", Status = "test" });
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<PostDTO>(It.IsAny<Post>())).Returns(new PostDTO());

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new GetPostByIdQueryHandler(mockRepo.Object, mockMapper.Object);
            var model = new PostDTO();

            int postId = 1;
            var request = new GetPostByIdQuery(postId);
            var post = await Task.FromResult(mockRepo.Object.Posts.GetByIdInclude(request.PostId));
            mockMapper.Object.Map<PostDTO>(post);
            var result = await handler.Handle(new GetPostByIdQuery(post.Id), CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<PostDTO>(result);
            Assert.Equal(model.Id, result.Id);
            Assert.Equal(model.Title, result.Title);
            Assert.Equal(model.Body, result.Body);
            Assert.Equal(model.FirstName, result.FirstName);
            Assert.Equal(model.LastName, result.LastName);
            Assert.Equal(model.AddedOn, result.AddedOn);
            Assert.Equal(model.Email, result.Email);
            Assert.Equal(model.Status, result.Status);

        }
    }
}
