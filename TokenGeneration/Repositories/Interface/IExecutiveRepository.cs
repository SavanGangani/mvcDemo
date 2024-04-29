using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Repository
{
    public interface IExecutiveRepository
    {
        Executive Login(Executive login);

        List<Customer> GetAllForUser(string username);
        Customer GetDataFromId(string id);

        void UpdateTask(Customer customer);

    }
}