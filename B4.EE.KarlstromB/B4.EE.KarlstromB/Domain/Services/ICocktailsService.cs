using B4.EE.KarlstromB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.EE.KarlstromB.Domain.Services
{
    public interface ICocktailsService
    {
        bool DataStoreExists { get; }
        Task<IQueryable<Cocktail>> GetAllCocktails();
        Task<Cocktail> GetCocktail(Guid id);
        Task<IQueryable<Cocktail>> GetCocktailsForUser(Guid userid);
        Task<Cocktail> UpdateCocktail(Cocktail cocktail);
        Task<Cocktail> AddCocktail(Cocktail cocktail);
        Task<Cocktail> DeleteCocktail(Guid id);
    }
}
