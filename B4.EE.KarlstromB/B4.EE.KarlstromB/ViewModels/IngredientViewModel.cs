using B4.EE.KarlstromB.Domain.Models;
using B4.EE.KarlstromB.Domain.Services;
using B4.EE.KarlstromB.Domain.Validators;
using FluentValidation;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.EE.KarlstromB.ViewModels
{
    public class IngredientViewModel : FreshBasePageModel
    {
        private Ingredient currentIngredient;
        private IValidator ingredientValidator;

        public IngredientViewModel()
        {
            ingredientValidator = new IngredientValidator();
        }

        #region Properties


        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; RaisePropertyChanged(nameof(PageTitle)); }
        }

        private string ingredientName;
        public string IngredientName
        {
            get { return ingredientName; }
            set { ingredientName = value; RaisePropertyChanged(nameof(IngredientName)); }
        }

        private int? amount;
        public int? Amount
        {
            get { return amount; }
            set { amount = value; RaisePropertyChanged(nameof(Amount)); }
        }

        private string optionalAmount;
        public string OptionalAmount
        {
            get { return optionalAmount; }
            set { optionalAmount = value; RaisePropertyChanged(nameof(OptionalAmount)); }
        }

        public IList<Ingredient> BasicIngredients { get { return IngredientData.BasicIngredients.OrderBy(e => e.Name).ToList(); } }


        private Ingredient selectedIngredient;
        public Ingredient SelectedIngredient
        {
            get { return selectedIngredient; }
            set { if (selectedIngredient != value) selectedIngredient = value; RaisePropertyChanged(nameof(SelectedIngredient)); }
        }

        private string ingredientNameError;
        public string IngredientNameError
        {
            get { return ingredientNameError; }
            set
            {
                ingredientNameError = value;
                RaisePropertyChanged(nameof(IngredientNameError));
                RaisePropertyChanged(nameof(IngredientNameErrorVisible));
            }
        }

        public bool IngredientNameErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(IngredientNameError); }
        }

        private string optionalAmountError;
        public string OptionalAmountError
        {
            get { return optionalAmountError; }
            set
            {
                optionalAmountError = value;
                RaisePropertyChanged(nameof(OptionalAmountError));
                RaisePropertyChanged(nameof(OptionalAmountErrorVisible));
            }
        }

        public bool OptionalAmountErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(OptionalAmountError); }
        }

        private string amountError;
        public string AmountError
        {
            get { return amountError; }
            set
            {
                amountError = value;
                RaisePropertyChanged(nameof(AmountError));
                RaisePropertyChanged(nameof(AmountErrorVisible));
            }
        }

        public bool AmountErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(AmountError); }
        }

        #endregion

        public override void Init(object initData)
        {
            Ingredient ingredient = initData as Ingredient;
            currentIngredient = ingredient;
            if (ingredient.Id == Guid.Empty)
            {
                PageTitle = "New ingredient";
            }
            else
            {
                PageTitle = "Edit ingredient";
            }
            LoadIngredient();
            base.Init(initData);
        }

        private void LoadIngredient()
        {
            IngredientName = currentIngredient.Name;
            Amount = currentIngredient.Amount;
            OptionalAmount = currentIngredient.OptionalAmount;
        }

        private void SaveIngredient()
        {
            if (SelectedIngredient == null)
            {
                currentIngredient.Name = IngredientName;
            }
            else
            {
                currentIngredient.Name = SelectedIngredient.Name;
            }
            currentIngredient.Amount = Amount;
            currentIngredient.OptionalAmount = OptionalAmount;
        }

        public ICommand SaveIngredientCommand => new Command(
            async () =>
            {
                try
                {
                    SaveIngredient();
                    if (Validate(currentIngredient))
                    {
                        if (currentIngredient.Id == Guid.Empty)
                        {
                            currentIngredient.Id = Guid.NewGuid();
                            currentIngredient.Cocktail.Ingredients.Add(currentIngredient);
                        }
                        else
                        {
                            currentIngredient.Cocktail.Ingredients.Add(currentIngredient);
                        }
                        await CoreMethods.PopPageModel(currentIngredient, false, true);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });

        private bool Validate(Ingredient ingredient)
        {
            IngredientNameError = "";
            AmountError = "";
            OptionalAmountError = "";

            var validationResult = ingredientValidator.Validate(ingredient);
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(ingredient.Name))
                {
                    IngredientNameError = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(ingredient.Amount))
                {
                    AmountError = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(ingredient.OptionalAmount))
                {
                    OptionalAmountError = error.ErrorMessage;
                }
            }

            return validationResult.IsValid;
        }
    }
}
