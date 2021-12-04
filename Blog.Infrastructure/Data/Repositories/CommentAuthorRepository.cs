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
    public class CommentAuthorRepository : Repository<CommentAuthor>, ICommentAuthorRepository
    {
        public CommentAuthorRepository(BlogDbContext context) : base(context)
        {
        }
    }
}
