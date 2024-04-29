using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories
{
    public interface IUserRepository
    {
        User Login(User user);
    }
}