﻿using B4.EE.KarlstromB.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace B4.EE.KarlstromB.Domain.Services.Local
{
    public class JsonUsersService : IUsersService
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonUsersService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "users.json");

            _serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public async Task<User> GetUser(Guid id)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(e => e.Id == id);
        }

        public async Task<User> UpdateUser(User user)
        {
            var users = await GetAllUsers();
            var userToUpdate = users.FirstOrDefault(e => e.Id == user.Id);
            users.Remove(userToUpdate);
            users.Add(user);
            SaveUsersToJsonFile(users);
            return users.FirstOrDefault(e => e.Id == user.Id);
        }

        public async Task<User> CreateUser(User user)
        {
            var users = await GetAllUsers();
            users.Add(user);
            SaveUsersToJsonFile(users);
            return await GetUser(user.Id);
        }

        protected async Task<IList<User>> GetAllUsers()
        {
            try
            {
                string usersJson = File.ReadAllText(_filePath);
                var users = JsonConvert.DeserializeObject<IList<User>>(usersJson);
                return await Task.FromResult(users);
            }
            catch
            {
                return new List<User>();
            }
        }

        protected void SaveUsersToJsonFile(IEnumerable<User> users)
        {
            string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented, _serializerSettings);
            File.WriteAllText(_filePath, usersJson);
        }

        public bool IsUnderage(User user)
        {
            return user?.Age < 18;
        }
    }
}
