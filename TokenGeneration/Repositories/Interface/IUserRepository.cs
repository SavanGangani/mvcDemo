using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Repositories.Models;

namespace Repositories.Repository
{
    public interface IUserRepository
    {
         int Insert(Customer customer);
       
       List<Customer> GetAllTask();
    }
}