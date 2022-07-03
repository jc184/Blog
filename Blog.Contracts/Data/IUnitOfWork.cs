using Blog.Contracts.Data.Repositories;

namespace Blog.Contracts.Data
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        Task CommitAsync();
    }
}
