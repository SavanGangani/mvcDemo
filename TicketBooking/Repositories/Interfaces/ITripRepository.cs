using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories
{
    public interface ITripRepository
    {
        List<Trip> GetAllTrip();
        void AddTrip(Trip trip);
        void UpdateTrip(Trip trip);

        void DeleteTrip(int id);

        Trip GetTripById(int id);

        List<Trip> GetUserTrip(int id);

        void BookTrip(Trip trip);

        List<Trip> MyTrip(int id);

        void Cancel(int id);
        // List<Trip> UserTrip(string trip);
    }
}