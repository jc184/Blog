using Blog.Contracts.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Validators
{
    public class UpdatePostDTOValidator : NullReferenceAbstractValidator<UpdatePostDTO>
    {
        public UpdatePostDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Body is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.AddedOn).NotEmpty().WithMessage("AddedOn is required");
        }
    }
}
