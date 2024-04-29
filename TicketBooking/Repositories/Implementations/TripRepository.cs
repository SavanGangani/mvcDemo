using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repositories;
using Repositories.Models;

namespace Repositories
{
    public class TripRepository : CommonRepository, ITripRepository
    {
        public TripRepository(IConfiguration myConfiguration) : base(myConfiguration)
        {
        }

        public List<Trip> GetAllTrip()
        {
            List<Trip> trips = new List<Trip>();

            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_tripid, c_trip, c_tripprice, c_triptotalticket, c_tripcurrentticket FROM t_trip115 ORDER BY c_tripid", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trip trip = new Trip();
                            trip.c_tripid = reader.GetInt32(0);
                            trip.c_trip = reader.GetString(1);
                            trip.c_tripprice = reader.GetInt32(2);
                            trip.c_triptotalticket = reader.GetInt32(3);
                            trip.c_tripcurrentticket = reader.GetInt32(4);
                            trips.Add(trip);
                        }
                    }
                }
                {

                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
            Console.WriteLine("Total Trip :" + trips.Count());
            return trips;
        }

        public void AddTrip(Trip trip)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO t_trip115 (c_trip, c_tripprice, c_triptotalticket, c_tripcurrentticket)VALUES(@trip, @price ,@total , @current)", conn))
                {
                    cmd.Parameters.AddWithValue("@trip", trip.c_trip);
                    cmd.Parameters.AddWithValue("@price", trip.c_tripprice);
                    cmd.Parameters.AddWithValue("@total", trip.c_triptotalticket);
                    cmd.Parameters.AddWithValue("@current", trip.c_tripcurrentticket);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteTrip(int id)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM t_trip115 WHERE c_tripid=@tripid", conn))
                {
                    cmd.Parameters.AddWithValue("@tripid", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public Trip GetTripById(int id)
        {
            Trip trip = new Trip();
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_tripid, c_trip, c_tripprice, c_triptotalticket, c_tripcurrentticket FROM t_trip115 WHERE c_tripid=@tripid", conn))
                {
                    cmd.Parameters.AddWithValue("@tripid", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trip.c_tripid = reader.GetInt32(0);
                            trip.c_trip = reader.GetString(1);
                            trip.c_tripprice = reader.GetInt32(2);
                            trip.c_triptotalticket = reader.GetInt32(3);
                            trip.c_tripcurrentticket = reader.GetInt32(4);
                        }
                    }
                }
            }

            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
            return trip;
        }


        public void UpdateTrip(Trip trip)
        {
            try
            {

                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE t_trip115 SET c_trip=@trip, c_tripprice=@price, c_triptotalticket=@total, c_tripcurrentticket=@currentti WHERE c_tripid=@tripid", conn))
                {
                    cmd.Parameters.AddWithValue("@trip", trip.c_trip);
                    cmd.Parameters.AddWithValue("@price", trip.c_tripprice);
                    cmd.Parameters.AddWithValue("@total", trip.c_triptotalticket);
                    cmd.Parameters.AddWithValue("@currentti", trip.c_tripcurrentticket);
                    cmd.Parameters.AddWithValue("@tripid", trip.c_tripid);
                    cmd.ExecuteNonQuery();

                }
                Console.WriteLine("Trip updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Trip> GetUserTrip(int id)
        {
            List<Trip> trips = new List<Trip>();

            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT c_tripid, c_trip, c_tripprice, c_triptotalticket, c_tripcurrentticket FROM t_trip115 WHERE c_tripid = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trip trip = new Trip();
                            trip.c_tripid = reader.GetInt32(0);
                            trip.c_trip = reader.GetString(1);
                            trip.c_tripprice = reader.GetInt32(2);
                            trip.c_triptotalticket = reader.GetInt32(3);
                            trip.c_tripcurrentticket = reader.GetInt32(4);
                            trips.Add(trip);
                        }
                    }
                }
                {

                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
            Console.WriteLine("Total Trip :" + trips.Count());
            return trips;
        }

        public void BookTrip(Trip trip)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO t_tripcust115 (c_tripuserid,c_tripid, c_tripdate, c_tripstatus, c_tripqty, c_totalcost) VALUES (@user,@trip,@date, @status, @qty, @cost)", conn))
                {
                    Console.WriteLine("IDDDDDDDDDD::::  "+ trip.userid);   
                    cmd.Parameters.AddWithValue("@user", trip.userid);
                    cmd.Parameters.AddWithValue("@trip", trip.c_tripid);
                    cmd.Parameters.AddWithValue("@date", trip.c_tripdate);
                    cmd.Parameters.AddWithValue("@qty", trip.c_tripqty);
                    cmd.Parameters.AddWithValue("@cost", trip.c_totalcost);
                    if (trip.c_tripdate < DateTime.Now)
                    {
                        cmd.Parameters.AddWithValue("@status", "Departed");

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@status", "Scheduled");
                    }
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand("UPDATE t_trip115 SET c_tripcurrentticket = @qty WHERE c_tripid=@tripid", conn))
                {
                    var qty = trip.c_tripcurrentticket - trip.c_tripqty;
                    Console.WriteLine("QTY : "+qty);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@tripid", trip.c_tripid);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Trip> MyTrip(int id){
            List<Trip> trips = new List<Trip>();

            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT cus.c_tripid, trip.c_trip, trip.c_tripprice, cus.c_tripdate, cus.c_tripqty, cus.c_totalcost, cus.c_tripcustid, cus.c_tripstatus FROM t_trip115 as trip , t_tripcust115 as cus WHERE cus.c_tripuserid = @userid AND cus.c_tripid = trip.c_tripid", conn))
                {
                    cmd.Parameters.AddWithValue("@userid", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Trip trip = new Trip();
                            trip.c_tripid = reader.GetInt32(0);
                            trip.c_trip = reader.GetString(1);
                            trip.c_tripprice = reader.GetInt32(2);
                            trip.c_tripdate = DateTime.Parse(reader.GetDateTime(3).ToString());
                            trip.c_tripqty = reader.GetInt32(4);
                            trip.c_totalcost = reader.GetInt32(5);
                            trip.c_tripcustid = reader.GetInt32(6);
                            trip.c_tripstatus = reader.GetString(7);
                            trips.Add(trip);
                        }
                    }
                }
                {

                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
            Console.WriteLine("Total Trip :" + trips.Count());
            return trips;
        }

        public void Cancel(int id)
        {
            try
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("UPDATE t_tripcust115 SET c_tripstatus = @status WHERE c_tripcustid=@tripid", conn))
                {
                    Console.WriteLine("NEW " +id);
                    cmd.Parameters.AddWithValue("@status","Cancelled");
                    cmd.Parameters.AddWithValue("@tripid", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}