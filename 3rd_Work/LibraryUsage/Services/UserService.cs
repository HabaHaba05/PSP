using LibraryUsage.Database;
using LibraryUsage.Mapper;
using LibraryUsage.Models;
using LibraryUsage.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryUsage.Services
{
    public class UserService : IUserService
    {
        // Not using "Repository class and writing queries to Db from here just for the sake of simplicity
        private readonly Context _context;
        private readonly IUserValidator _userValidator;
        public UserService(Context context, IUserValidator userValidator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
        }

        public async Task<User> Create(CreateUser createUser)
        {
            if (!_userValidator.IsValid(createUser))
            {
                return null;
            }

            User user = createUser.ConvertToUser();

            _context.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteById(int id)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                return null;
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public async Task<User> Update(int id, CreateUser updateUser)
        {
            User userToUpdate = _context.Users.FirstOrDefault(x => x.Id == id);

            if (!_userValidator.IsValid(updateUser) || userToUpdate is null)
            {
                return null;
            }

            userToUpdate.Name = updateUser.Name;
            userToUpdate.Surname = updateUser.Surname;
            userToUpdate.Password = updateUser.Password;
            userToUpdate.PhoneNr = updateUser.PhoneNr;
            userToUpdate.Address = updateUser.Address;
            userToUpdate.Email = updateUser.Email;

            _context.Update(userToUpdate);
            await _context.SaveChangesAsync();

            return userToUpdate;
        }
    }
}
