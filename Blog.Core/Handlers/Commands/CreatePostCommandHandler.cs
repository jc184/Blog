using Blog.Contracts.Data;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using Blog.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace Blog.Core.Handlers.Commands
{
    public class CreatePostCommand : IRequest<int>
    {
        public CreatePostDTO Model { get; }
        public CreatePostCommand(CreatePostDTO model)
        {
            this.Model = model;
        }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreatePostDTO> _validator;

        public CreatePostCommandHandler(IUnitOfWork repository, IValidator<CreatePostDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            CreatePostDTO model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = new Post
            {
                Title = model.Title,
                Body = model.Body,
                Status = model.Status,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,

            };

            _repository.Posts.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}
