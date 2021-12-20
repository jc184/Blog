using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string? Content { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int PostId { get; set; }
    }
}
