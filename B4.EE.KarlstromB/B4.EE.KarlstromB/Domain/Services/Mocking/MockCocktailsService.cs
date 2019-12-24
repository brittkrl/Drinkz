using B4.EE.KarlstromB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Services.Mocking
{
    public class MockCocktailsService
    {
        private List<Cocktail> basicCocktails;

        public MockCocktailsService()
        {
            basicCocktails = new List<Cocktail>();

            basicCocktails.Add(new Cocktail()
            {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000001"),
                Name = "Cosmopolitan",
                Preparation = "Fill shaker with ice. Add 40ml of vodka, 15ml Cointreau, 15ml lime juice and 30ml cranberry juice. Shake well and strain into cocktail glass. Garnish with a lime slice.",
                UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                Rating = 8,
                ImageUrl = "cosmopolitan.jpg",
                IsEditable = false,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000001"),
                        Name = "Vodka",
                        Amount = 40
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000002"),
                        Name = "Cointreau",
                        Amount = 15
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000003"),
                        Name = "Cranberry juice",
                        Amount = 30
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000004"),
                        Name = "Lime juice",
                        Amount = 15
                    }
                }
            });;

            basicCocktails.Add(new Cocktail()
            {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000002"),
                Name = "Negroni",
                Preparation = "Stir all ingredients with ice.",
                UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                Rating = 8,
                ImageUrl = "negroni.jpg",
                IsEditable = false,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000004"),
                        Name = "Gin",
                        Amount = 30
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000005"),
                        Name = "Campari",
                        Amount = 30
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000006"),
                        Name = "Red Vermouth",
                        Amount = 30
                    }
                }
            });

            basicCocktails.Add(new Cocktail()
            {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000003"),
                Name = "Manhattan",
                Preparation = "Stir ingredients in a mixing glass with ice. Strain into chilled martini glass or cocktail coupe.",
                UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                Rating = 7,
                ImageUrl = "manhattan.jpg",
                IsEditable = false,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000007"),
                        Name = "Whiskey",
                        Amount = 60
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000008"),
                        Name = "Red Vermouth",
                        Amount = 30
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000009"),
                        Name = "Angostura bitters",
                        OptionalAmount = "2 dashes"
                    }                    
                }
            });

            basicCocktails.Add(new Cocktail()
            {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000004"),
                Name = "Dark 'n Stormy",
                Preparation = "Fill a highball glass with ice and add rum and ginger beer. Garnish with lime.",
                UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                Rating = 9,
                ImageUrl = "darknstormy.jpg",
                IsEditable = false,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000010"),
                        Name = "Dark Rum",
                        Amount = 45
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000011"),
                        Name = "Ginger Ale",
                        Amount = 120
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000012"),
                        Name = "Angostura bitters",
                        OptionalAmount = "1 dash"
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000013"),
                        Name = "Lime juice",
                        Amount = 30
                    }
                }
            });

            basicCocktails.Add(new Cocktail()
            {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000005"),
                Name = "Zombie",
                Preparation = "Pour the rums and fruit juices into a cocktail shaker filled with ice and shake hard until the outside of the shaker feels really cold. Strain the mixture into a tall glass or hurricane glass filled with ice, then slowly pour in the grenadine to colour the drink.",
                UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                Rating = 10,
                ImageUrl = "zombie.jpg",
                IsEditable = false,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000014"),
                        Name = "Dark Rum",
                        Amount = 15
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000015"),
                        Name = "Golden rum",
                        Amount = 15
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000016"),
                        Name = "White rum",
                        Amount = 15
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000017"),
                        Name = "Cointreau",
                        Amount = 15
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000018"),
                        Name = "Pineapple juice",
                        Amount = 150
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000019"),
                        Name = "Lime juice",
                        Amount = 50
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000020"),
                        Name = "Grenadine",
                        Amount = 10
                    }
                }
            });

            basicCocktails.Add(new Cocktail()
            {
                Id = Guid.Parse("00000000-1111-0000-0000-000000000006"),
                Name = "Margarita",
                Preparation = "Since this recipe includes fresh juice, it should be shaken. Serve over ice in a glass with a salted rim.",
                UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                Rating = 8,
                ImageUrl = "margarita.jpg",
                IsEditable = false,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000021"),
                        Name = "Silver tequila",
                        Amount = 60
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000022"),
                        Name = "Cointreau",
                        Amount = 30
                    },
                    new Ingredient
                    {
                        Id = Guid.Parse("00000000-1111-1111-0000-000000000023"),
                        Name = "Lime juice",
                        Amount = 30
                    }
                }
            });
        }
        public IEnumerable<Cocktail> GetAll()
        {
            return basicCocktails;
        }

        public Cocktail GetById(Guid id)
        {
            return basicCocktails.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
