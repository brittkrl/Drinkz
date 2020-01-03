using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Models
{
    public static class IngredientData
    {
        public static IList<Ingredient> BasicIngredients { get; private set; }

        static IngredientData()
        {
            BasicIngredients = new List<Ingredient>();

            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Bailey's"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Blue Curacao"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Cointreau"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Cola"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Dark Rum"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Gin"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Pineapple juice"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Tonic"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Vodka"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "White rum"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Whiskey"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "7-Up"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Amaretto"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Apricot Brandy"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Bourbon"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Brandy"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Campari"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Coconut cream"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Coconut milk"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Coffee Liqueur"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Cognac"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Ginger Ale"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Gold Rum"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Kahlúa"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Orange juice"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Pineapple"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Red Vermouth"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Sambuca"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Sugar syrup"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Tequila Blanco"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Tomato juice"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Cream"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Licor 43"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Red Bull"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Red Vodka"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Ice"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Lime"
            });
            BasicIngredients.Add(new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Angostura bitters"
            });
        }
    }
}
