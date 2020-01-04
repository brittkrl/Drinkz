using B4.EE.KarlstromB.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(bucket => bucket.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a valid name");

            RuleFor(bucket => bucket.LastName)
                .NotEmpty()
                .WithMessage("Please enter a valid name");
        }
    }
}
