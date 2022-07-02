using Blog.Contracts.Data;
using Blog.Contracts.DTO;
using Blog.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace Blog.Core.Handlers.Commands
{
    public class UpdatePostCommand : IRequest<int>
    {
        public int Id { get; set; }
        public UpdatePostDTO Model { get; }
        public UpdatePostCommand(UpdatePostDTO model, int id)
        {
            this.Model = model;
            this.Id = id;
        }
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<UpdatePostDTO> _validator;

        public UpdatePostCommandHandler(IUnitOfWork repository, IValidator<UpdatePostDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            UpdatePostDTO model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entityToUpdate = _repository.Posts.Get(request.Id);
            if (entityToUpdate == null)
            {
                return default;
            }
            else
            {
                entityToUpdate.Title = model.Title;
                entityToUpdate.Body = model.Body;
                entityToUpdate.FirstName = model.FirstName;
                entityToUpdate.LastName = model.LastName;
                entityToUpdate.Email = model.Email;
                entityToUpdate.AddedOn = model.AddedOn;
                _repository.Posts.Update(entityToUpdate);
                await _repository.CommitAsync();

                return entityToUpdate.Id;
            }

        }
    }
}
