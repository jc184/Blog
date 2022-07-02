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
    public class UpdatePostCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Returns_Int()
        {
            //Arrange
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(x => x.Posts.Add(It.IsAny<Post>()));
            var mockValidator = new Mock<IValidator<UpdatePostDTO>>();


            var value = new FluentValidation.Results.ValidationResult();
            mockValidator.Setup(x => x.Validate(It.IsAny<UpdatePostDTO>())).Returns(value);

            var mediator = new Mock<IMediator>();
            //Act
            var handler = new UpdatePostCommandHandler(mockRepo.Object, mockValidator.Object);
            var model = new UpdatePostDTO() { Title = "Test", Body = "Test", FirstName = "TestName", LastName = "TestName", Email = "jc123@gmail.com", Status = "test" };
            int id = 0;
            var request = new UpdatePostCommand(model, id);
            var requestModel = request.Model;

            var validModel = mockValidator.Object.Validate(model);
            var entity = new Post();
            mockRepo.Object.Posts.Update(entity);

            var result = await handler.Handle(new UpdatePostCommand(requestModel, id), CancellationToken.None);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(0, result);
        }
    }
}
