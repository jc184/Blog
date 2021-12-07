using Blog.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Migrations
{
    public class IncludeExtension
    {
        private readonly BlogDbContext _context;
        private readonly DbSet<Post> _dbSet;

        public IncludeExtension(BlogDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Post>();
        }
        public IQueryable<Post> Include(params Expression<Func<Post, object>>[] includes)
        {
            IIncludableQueryable<Post, object> query = null;

            if (includes.Length > 0)
            {
                query = _dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? _dbSet : (IQueryable<Post>)query;
        }

    }
}
