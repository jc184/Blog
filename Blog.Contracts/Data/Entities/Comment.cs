﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime AddedOn { get; set; }
        public Author Author { get; set; }
    }
}
