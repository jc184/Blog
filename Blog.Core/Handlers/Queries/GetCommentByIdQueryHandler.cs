using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.DTO;
using Blog.Core.Exceptions;
using MediatR;

namespace Blog.Core.Handlers.Queries
{
    public class GetCommentByIdQuery : IRequest<CommentDTO>
    {
        public int CommentId { get; }
        public GetCommentByIdQuery(int commentId)
        {
            CommentId = commentId;
        }
    }

    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommentDTO> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await Task.FromResult(_repository.Comments.Get(request.CommentId));

            if (comment == null)
            {
                throw new EntityNotFoundException($"No comment found for Id {request.CommentId}");
            }

            return _mapper.Map<CommentDTO>(comment);
        }
    }
}
