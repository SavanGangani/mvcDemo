using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Repository
{
    public interface IUserRepository
    {
         User GetOne(int id);
        bool Register(User user);
        void Update(User user);
        User Login(string email , string password);

        bool IsEmailExists(string c_useremail);
    }
}