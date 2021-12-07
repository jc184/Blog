using Blog.Contracts.Data.Entities;
using Blog.Contracts.DTO;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Validators
{
    public class CreateCommentDTOValidator : NullReferenceAbstractValidator<CreateCommentDTO>
    {
        public CreateCommentDTOValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}
