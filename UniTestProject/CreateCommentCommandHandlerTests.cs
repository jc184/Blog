using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using Blog.Core.Handlers.Commands;
using FluentValidation;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTestProject
{
    public class CreateCommentCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Int()
        {
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Comments.Add(It.IsAny<Comment>()));
            var mockValidator = new Mock<IValidator<CreateCommentDTO>>();


            var value = new FluentValidation.Results.ValidationResult();
            mockValidator.Setup(x => x.Validate(It.IsAny<CreateCommentDTO>())).Returns(value);

            var mediator = new Mock<IMediator>();
            var handler = new CreateCommentCommandHandler(mockRepo.Object, mockValidator.Object);
            var model = new CreateCommentDTO() { Content = "Test", FirstName = "TestName", LastName = "TestName", Email = "jc123@gmail.com", PostId = 1 };
            var request = new CreateCommentCommand(model);
            var requestModel = request.Model;
           
            var validModel = mockValidator.Object.Validate(model);
            var entity = new Comment();
            mockRepo.Object.Comments.Add(entity);

            var result = await handler.Handle(new CreateCommentCommand(requestModel), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<int>(result);
            Assert.Equal(0, result);
        }
    }
}
