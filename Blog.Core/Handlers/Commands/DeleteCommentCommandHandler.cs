using Blog.Contracts.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Handlers.Commands
{
    public class DeleteCommentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteCommentCommand(int id)
        {
            this.Id = id;
        }
    }
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public DeleteCommentCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {

            _repository.Comments.Delete(request.Id);
            await _repository.CommitAsync();

            return request.Id;
        }
    }
}
