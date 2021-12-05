using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.DTO;
using Blog.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Handlers.Queries
{
    public class GetPostByIdQuery : IRequest<PostDTO>
    {
        public int PostId { get; }
        public GetPostByIdQuery(int postId)
        {
            PostId = postId;
        }
    }

    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PostDTO> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await Task.FromResult(_repository.Posts.Get(request.PostId));

            if (post == null)
            {
                throw new EntityNotFoundException($"No post found for Id {request.PostId}");
            }

            return _mapper.Map<PostDTO>(post);
        }
    }
}
