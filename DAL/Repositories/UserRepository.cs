using DAL.Context;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected SignalRContext _dbContext;

        public UserRepository(SignalRContext dbContext)
        {
            _dbContext = dbContext;
        }     

        public User GetUserByUsername(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == username);

            return user;
        }

        public IEnumerable<Claim> GetUserClaimsByUserId(Guid userId)
        {
            // get user with claims
            var user = _dbContext.Users.Include("Claims").FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return new List<Claim>();
            }
            return user.Claims.ToList();
        }

        public bool IsUserActive(Guid id)
        {
            var user = _dbContext.Users.Find(id);
            return user.IsActive;
        }
    }
}
