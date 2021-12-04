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
        IAuthorRepository Authors { get; }
        Task CommitAsync();
    }
}
