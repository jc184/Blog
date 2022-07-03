using Blog.Contracts.Data;
using Blog.Contracts.Data.Repositories;
using Blog.Infrastructure.Data.Repositories;
using Blog.Migrations;

namespace Blog.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _context;

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
        }
        public IPostRepository Posts => new PostRepository(_context);
        public ICommentRepository Comments => new CommentRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
