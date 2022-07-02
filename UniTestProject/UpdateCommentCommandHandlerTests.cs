using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using Blog.Core.Handlers.Commands;
using FluentValidation;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    public class UpdateCommentCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Int()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Comments.Add(It.IsAny<Comment>()));
            var mockValidator = new Mock<IValidator<UpdateCommentDTO>>();


            var value = new FluentValidation.Results.ValidationResult();
            mockValidator.Setup(x => x.Validate(It.IsAny<UpdateCommentDTO>())).Returns(value);

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new UpdateCommentCommandHandler(mockRepo.Object, mockValidator.Object);
            var model = new UpdateCommentDTO() { Content = "Test", FirstName = "TestName", LastName = "TestName", Email = "jc123@gmail.com", PostId = 1 };
            int id = 0;
            var request = new UpdateCommentCommand(model, id);
            var requestModel = request.Model;

            var validModel = mockValidator.Object.Validate(model);
            var entity = new Comment();
            mockRepo.Object.Comments.Update(entity);

            var result = await handler.Handle(new UpdateCommentCommand(requestModel, id), CancellationToken.None);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(0, result);
        }
    }
}
