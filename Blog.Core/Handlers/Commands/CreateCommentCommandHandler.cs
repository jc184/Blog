using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using Blog.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace Blog.Core.Handlers.Commands
{
    public class CreateCommentCommand : IRequest<int>
    {
        public CreateCommentDTO Model { get; }
        public CreateCommentCommand(CreateCommentDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateCommentDTO> _validator;

        public CreateCommentCommandHandler(IUnitOfWork repository, IValidator<CreateCommentDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            CreateCommentDTO model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = new Comment
            {
                Content = model.Content,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PostId = model.PostId,

            };

            _repository.Comments.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}
