using Blog.Contracts.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Data
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        IPostAuthorRepository PostAuthors { get; }
        ICommentAuthorRepository CommentAuthors { get; }
        Task CommitAsync();
    }
}
