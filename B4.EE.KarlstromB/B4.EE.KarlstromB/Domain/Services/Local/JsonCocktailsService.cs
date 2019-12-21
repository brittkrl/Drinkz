﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B4.EE.KarlstromB.Domain.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace B4.EE.KarlstromB.Domain.Services.Local
{
     public class JsonCocktailsService : ICocktailsService
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonCocktailsService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "cocktails.json");

            _serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public async Task<Cocktail> AddCocktail(Cocktail cocktail)
        {
            var cocktails = await GetAllCocktails();
            cocktails.Add(cocktail);
            SaveCocktailsToJsonFile(cocktails);
            return await GetCocktail(cocktail.Id);
        }

        public async Task<Cocktail> DeleteCocktail(Guid id)
        {
            var cocktails = await GetAllCocktails();
            var cocktailToRemove = cocktails.FirstOrDefault(e => e.Id == id);
            cocktails.Remove(cocktailToRemove);
            SaveCocktailsToJsonFile(cocktails);
            return cocktailToRemove;
        }

        public async Task<Cocktail> GetCocktail(Guid id)
        {
            var cocktails = await GetAllCocktails();
            return cocktails.FirstOrDefault(e => e.Id == id);
        }       

        public async Task<IQueryable<Cocktail>> GetCocktailsForUser(Guid userid)
        {
            var cocktails = await GetAllCocktails();
            return cocktails.Where(e => e.UserId == userid).AsQueryable();
        }

        public async Task<Cocktail> UpdateCocktail(Cocktail cocktail)
        {
            await DeleteCocktail(cocktail.Id);
            return await AddCocktail(cocktail);
        }

        protected async Task<IList<Cocktail>> GetAllCocktails()
        {
            try
            {
                string cocktailsJson = File.ReadAllText(_filePath);
                var cocktails = JsonConvert.DeserializeObject<IList<Cocktail>>(cocktailsJson);
                return await Task.FromResult(cocktails);
            }
            catch
            {
                return (new List<Cocktail>());
            }
        }

        protected void SaveCocktailsToJsonFile(IEnumerable<Cocktail> cocktails)
        {
            string cocktailsJson = JsonConvert.SerializeObject(cocktails, Formatting.Indented, _serializerSettings);
            File.WriteAllText(_filePath, cocktailsJson);
        }
    }
}
