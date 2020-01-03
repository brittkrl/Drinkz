using B4.EE.KarlstromB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Services
{
    public class DataSeederService : ISeederService
    {
        private ICocktailsService _cocktailsService;

        public DataSeederService(ICocktailsService cocktailsService)
        {
            _cocktailsService = cocktailsService;
        }
        public void EnsureSeeded()
        {
            if (!_cocktailsService.DataStoreExists)
            {
                _cocktailsService.AddCocktail(
                    new Cocktail
                    {
                        Id = Guid.Parse("00000000-1111-0000-0000-000000000001"),
                        Name = "Cosmopolitan",
                        Preparation = "Fill shaker with ice. Add 40ml of vodka, 15ml Cointreau, 15ml lime juice and 30ml cranberry juice. Shake well and strain into cocktail glass. Garnish with a lime slice.",
                        UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                        Rating = 8,
                        ImageUrl = "cosmopolitan.jpg",
                        Ingredients = new ObservableCollection<Ingredient>()
                        {
                            new Ingredient
                            {
                                Name = "Vodka",
                                Amount = 40
                            },
                            new Ingredient
                            {
                                Name = "Cointreau",
                                Amount = 15
                            },
                            new Ingredient
                            {
                                Name = "Cranberry juice",
                                Amount = 30
                            },
                            new Ingredient
                            {
                                Name = "Lime juice",
                                Amount = 15
                            }
                        }
                    });

                _cocktailsService.AddCocktail(
                    new Cocktail
                    {
                        Id = Guid.Parse("00000000-1111-0000-0000-000000000002"),
                        Name = "Negroni",
                        Preparation = "Stir all ingredients with ice.",
                        UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                        Rating = 8,
                        ImageUrl = "negroni.jpg",
                        Ingredients = new ObservableCollection<Ingredient>()
                        {
                            new Ingredient
                            {
                                Name = "Gin",
                                Amount = 30
                            },
                            new Ingredient
                            {
                                Name = "Campari",
                                Amount = 30
                            },
                            new Ingredient
                            {
                                Name = "Red Vermouth",
                                Amount = 30
                            }
                        }
                    });

                _cocktailsService.AddCocktail(
                    new Cocktail
                    {
                        Id = Guid.Parse("00000000-1111-0000-0000-000000000003"),
                        Name = "Manhattan",
                        Preparation = "Stir ingredients in a mixing glass with ice. Add 2 dashes of Angostura bitters. Strain into chilled martini glass or cocktail coupe.",
                        UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                        Rating = 7,
                        ImageUrl = "manhattan.jpg",
                        Ingredients = new ObservableCollection<Ingredient>()
                        {
                            new Ingredient
                            {
                                Name = "Whiskey",
                                Amount = 60
                            },
                            new Ingredient
                            {
                                Name = "Red Vermouth",
                                Amount = 30
                            },
                            new Ingredient
                            {
                                Name = "Angostura bitters",
                                OptionalAmount = "2 dashes"
                            }
                        }
                    });

                _cocktailsService.AddCocktail(
                    new Cocktail
                    {
                        Id = Guid.Parse("00000000-1111-0000-0000-000000000004"),
                        Name = "Dark 'n Stormy",
                        Preparation = "Fill a highball glass with ice and add rum and ginger beer. Add 1 dash of Angostura bitters. Garnish with lime.",
                        UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                        Rating = 9,
                        ImageUrl = "darknstormy.jpg",
                        Ingredients = new ObservableCollection<Ingredient>()
                        {
                            new Ingredient
                            {
                                Name = "Dark Rum",
                                Amount = 45
                            },
                            new Ingredient
                            {
                                Name = "Ginger Ale",
                                Amount = 120
                            },
                            new Ingredient
                            {
                                Name = "Angostura bitters",
                                OptionalAmount = "1 dash"
                            },
                            new Ingredient
                            {
                                Name = "Lime juice",
                                Amount = 30
                            }
                        }
                    });

                _cocktailsService.AddCocktail(
                    new Cocktail
                    {
                        Id = Guid.Parse("00000000-1111-0000-0000-000000000005"),
                        Name = "Zombie",
                        Preparation = "Pour the rums and fruit juices into a cocktail shaker filled with ice and shake hard until the outside of the shaker feels really cold. Strain the mixture into a tall glass or hurricane glass filled with ice, then slowly pour in the grenadine to colour the drink.",
                        UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                        Rating = 10,
                        ImageUrl = "zombie.jpg",
                        Ingredients = new ObservableCollection<Ingredient>()
                        {
                            new Ingredient
                            {
                                Name = "Dark Rum",
                                Amount = 15
                            },
                            new Ingredient
                            {
                                Name = "Golden rum",
                                Amount = 15
                            },
                            new Ingredient
                            {
                                Name = "White rum",
                                Amount = 15
                            },
                            new Ingredient
                            {
                                Name = "Cointreau",
                                Amount = 15
                            },
                            new Ingredient
                            {
                                Name = "Pineapple juice",
                                Amount = 150
                            },
                            new Ingredient
                            {
                                Name = "Lime juice",
                                Amount = 50
                            },
                            new Ingredient
                            {
                                Name = "Grenadine",
                                Amount = 10
                            }
                        }
                    });

                _cocktailsService.AddCocktail(
                    new Cocktail
                    {
                        Id = Guid.Parse("00000000-1111-0000-0000-000000000006"),
                        Name = "Margarita",
                        Preparation = "Since this recipe includes fresh juice, it should be shaken. Serve over ice in a glass with a salted rim.",
                        UserId = Guid.Parse("00000000-1111-2222-0000-000000000001"),
                        Rating = 8,
                        ImageUrl = "margarita.jpg",
                        Ingredients = new ObservableCollection<Ingredient>()
                        {
                            new Ingredient
                            {
                                Name = "Silver tequila",
                                Amount = 60
                            },
                            new Ingredient
                            {
                                Name = "Cointreau",
                                Amount = 30
                            },
                            new Ingredient
                            {
                                Name = "Lime juice",
                                Amount = 30
                            }
                        }
                    });
            }
        }
    }
}
