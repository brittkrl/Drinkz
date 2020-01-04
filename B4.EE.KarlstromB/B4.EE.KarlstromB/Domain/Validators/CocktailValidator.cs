using B4.EE.KarlstromB.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Validators
{
    public class CocktailValidator : AbstractValidator<Cocktail>
    {
        public CocktailValidator()
        {
            RuleFor(cocktail => cocktail.Name)
               .NotEmpty()
               .WithMessage("Name cannot be empty")
               .Length(3, 30)
               .WithMessage("Length must be between 3 and 30");
        }
    }
}
