using Blog.Contracts.Data.Entities;
using Blog.Contracts.Data.Repositories;
using Blog.Infrastructure.Data.Repositories.Generic;
using Blog.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDbContext context) : base(context)
        {
        }
    }
}
