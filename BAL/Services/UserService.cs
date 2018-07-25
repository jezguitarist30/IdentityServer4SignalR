using BAL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByUsername(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                throw new NullReferenceException();
            }

            return _userRepository.GetUserByUsername(username);
        }

        public Task<bool> IsUserCredentialsValidAsync(string username, string password)
        {
            // get the user
            var user = GetUserByUsername(username);

            if (user == null)
            {
                return Task.FromResult(false);
            }

            // check a password
            bool validPassword = password == user.Password;

            return Task.FromResult(validPassword);
        }
    }
}
