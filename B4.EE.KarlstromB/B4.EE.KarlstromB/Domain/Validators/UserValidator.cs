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
            RuleFor(user => user.EmailAddress)
                .NotEmpty()
                    .WithMessage("E-mail cannot be empty")
                .EmailAddress()
                    .WithMessage("Please enter a valid e-mail address");

            RuleFor(bucket => bucket.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a valid name");

            RuleFor(bucket => bucket.LastName)
                .NotEmpty()
                .WithMessage("Please enter a valid name");
        }
    }
}
