using Blog.Contracts.Data.Entities;
using Blog.Contracts.Data.Repositories;
using Blog.Infrastructure.Data.Repositories.Generic;
using Blog.Migrations;

namespace Blog.Infrastructure.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
        {
        }
    }
}
