using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Data.Entities
{
    public class Post : BaseEntity
    {
        public string? Title { get; set; }
        public string? Body { get; set; } 
        public string? Status { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public ICollection<Comment>? Comments { get; set;}

    }
}
