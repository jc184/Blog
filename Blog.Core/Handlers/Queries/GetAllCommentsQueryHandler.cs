using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.DTO;
using MediatR;

namespace Blog.Core.Handlers.Queries
{

    public class GetAllCommentsQuery : IRequest<IEnumerable<CommentDTO>>
    {
    }

    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllCommentsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDTO>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Comments.GetAll());
            return _mapper.Map<IEnumerable<CommentDTO>>(entities);
        }
    }
}
