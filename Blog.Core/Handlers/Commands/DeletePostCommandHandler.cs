using Blog.Contracts.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Handlers.Commands
{
    public class DeletePostCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeletePostCommand(int id)
        {
            this.Id = id;
        }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public DeletePostCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {

            _repository.Posts.Delete(request.Id);
            await _repository.CommitAsync();

            return request.Id;
        }
    }
}
