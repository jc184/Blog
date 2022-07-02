using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using Blog.Core.Handlers.Queries;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public class GetCommentByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_CommentDTO()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            int id = 1;
            mockRepo.Setup(x => x.Comments.Get(id)).Returns(new Comment() {  Id = 1, Content = "content", FirstName = "test", LastName = "test", Email = "jc123@gmail.com", AddedOn = System.DateTime.Now, PostId = 1});
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CommentDTO>(It.IsAny<Comment>())).Returns(new CommentDTO());

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new GetCommentByIdQueryHandler(mockRepo.Object, mockMapper.Object);
            var model = new CommentDTO();
            
            int commentId = 1;
            var request = new GetCommentByIdQuery(commentId);
            var comment = await Task.FromResult(mockRepo.Object.Comments.Get(request.CommentId));
            //var mappedComment = mockMapper.Object.Map(mockRepo.Object, comment);
            mockMapper.Object.Map<CommentDTO>(comment);
            var result = await handler.Handle(new GetCommentByIdQuery(comment.Id), CancellationToken.None);

            //Assert
            Assert.IsAssignableFrom<CommentDTO>(result);
            Assert.Equal(model.Id, result.Id);
            Assert.Equal(model.FirstName, result.FirstName);
            Assert.Equal(model.LastName, result.LastName);
            Assert.Equal(model.Content, result.Content);
            Assert.Equal(model.Email, result.Email);
            Assert.Equal(model.AddedOn, result.AddedOn);
            
        }
    }
}
