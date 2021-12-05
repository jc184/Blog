using AutoMapper;
using Blog.Contracts.Data;
using Blog.Contracts.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Handlers.Queries
{
    public class GetAllPostsQuery : IRequest<IEnumerable<PostDTO>>
    {
    }

    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDTO>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Posts.GetAll().Include(x => x.Comments).ToList());
            return _mapper.Map<IEnumerable<PostDTO>>(entities);
        }
    }
}
