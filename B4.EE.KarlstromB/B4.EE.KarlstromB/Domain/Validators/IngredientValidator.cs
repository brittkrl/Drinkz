using B4.EE.KarlstromB.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Validators
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        private Ingredient ingredient;

        public IngredientValidator()
        {
            ingredient = new Ingredient();
            RuleFor(ingredient => ingredient.Name)
               .NotEmpty()
               .WithMessage("Name cannot be empty");

            //if (ingredient.OptionalAmount == null)
            //{
            //    RuleFor(ingredient => ingredient.Amount)
            //   .NotEmpty()
            //   .WithMessage("Amount cannot be empty");
            //}

            //if (ingredient.Amount == null)
            //{
            //    RuleFor(ingredient => ingredient.OptionalAmount)
            //   .NotEmpty()
            //   .WithMessage("Optional amount cannot be empty");
            //}
        }
    }
}
