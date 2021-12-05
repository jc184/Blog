using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;

namespace Blog.Core.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CreatePostDTO, Post>();
            CreateMap<UpdatePostDTO, Post>();
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<UpdateCommentDTO, Comment>();
        }
    }
}
