﻿using Blog.Contracts.DTO;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Validators
{
    public class NullReferenceAbstractValidator<T> : AbstractValidator<T>
    {
        public ValidationResult Validate(T instance)
        {
            return instance == null
                ? new ValidationResult(new[] { new ValidationFailure(instance.ToString(), "response cannot be null", "Error") })
                : base.Validate(instance);
        }
    }
}