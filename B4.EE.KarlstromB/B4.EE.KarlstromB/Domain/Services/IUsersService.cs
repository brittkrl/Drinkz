using B4.EE.KarlstromB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace B4.EE.KarlstromB.Domain.Services
{
    public interface IUsersService
    {
        Task<User> GetUser(Guid id);
        Task<User> UpdateUser(User user);
        Task<User> CreateUser(User user);
    }
}
