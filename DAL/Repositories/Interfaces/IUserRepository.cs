using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {        
        User GetUserByUsername(string username);
        IEnumerable<Claim> GetUserClaimsByUserId(Guid id);
        bool IsUserActive(Guid id);
    }
}
