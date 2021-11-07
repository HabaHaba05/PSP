using LibraryUsage.Mapper;
using LibraryUsage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryUsage.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int id);
        Task<User> DeleteById(int id);
        Task<User> Update(int id, CreateUser user);
        Task<User> Create(CreateUser user);
    }
}
