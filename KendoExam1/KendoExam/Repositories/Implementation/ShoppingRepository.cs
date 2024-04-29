using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repositories.Models;

namespace Repositories.Repository
{
    public class ShoppingRepository : CommonRepository , IShoppingRepository
    {
          private IHttpContextAccessor _httpContextAccessor;
        public ShoppingRepository(IConfiguration myConfiguration, IHttpContextAccessor httpContextAccessor) : base(myConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        List<Album> IShoppingRepository.GetAlbums()
        {
            List<Album> albums = new List<Album>();
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("SELECT c_albumid, c_image, c_type, c_artist, c_title, c_price FROM t_albums115;", conn);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var album = new Album
                    {
                        c_albumid = reader.GetInt32(0),
                        c_image = reader.GetString(1),
                        c_type = reader.GetString(2),
                        c_artist = reader.GetString(3),
                        c_title = reader.GetString(4),
                        c_price = reader.GetDecimal(5)
                    };
                    albums.Add(album);
                }
                return albums;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while fetching All the Albums .." + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }


  

        bool IShoppingRepository.AddToCart(Shopping shop)
        {
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("INSERT INTO public.t_album_shopping115(c_albumid, c_userid, c_username, c_quantity, c_total_price) VALUES (@albumid, @userid, @username, @quantity, @total);", conn);
                command.Parameters.AddWithValue("@albumid", shop.c_albumid);
                command.Parameters.AddWithValue("@userid", shop.c_userid);
                command.Parameters.AddWithValue("@username", shop.c_username);
                command.Parameters.AddWithValue("@quantity", shop.c_quantity);
                command.Parameters.AddWithValue("@total", shop.c_total_price);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Add to cart !" + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        List<Shopping> IShoppingRepository.GetAllCart(int userid)
        {
            List<Shopping> Allcart = new List<Shopping>();
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("select  s.c_shoppingid, s.c_albumid ,s.c_userid ,a.c_title , a.c_price , s.c_quantity , s.c_total_price from t_album_shopping115 s INNER JOIN t_albums115 a ON s.c_albumid = a.c_albumid where s.c_userid = @userid;", conn);
                command.Parameters.AddWithValue("userid", userid);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var cart = new Shopping
                    {
                        c_shoppingid = reader.GetInt32(0),
                        c_albumid = reader.GetInt32(1),
                        c_userid = reader.GetInt32(2),
                        c_quantity = reader.GetInt32(5),
                        c_total_price = reader.GetDouble(6),
                        c_title = reader.GetString(3),
                        c_price = reader.GetDouble(4)

                    };
                    Allcart.Add(cart);
                }
                return Allcart;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting all the Cart!" + ex.Message);
                return null;

            }
            finally
            {
                conn.Close();
            }
        }



        bool IShoppingRepository.RemoveCart(int cartId)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server=cipg01;Port=5432;Database=GROUPD;User Id=postgres;Password=123456");
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("DELETE FROM public.t_album_shopping115 WHERE c_shoppingid = @id;", conn);
                command.Parameters.AddWithValue("@id", cartId);
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Removing from Cart: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }


        long IShoppingRepository.Placeorder(CheckOut order)
        {
            Int64 orderId = 0;
            NpgsqlConnection conn = new NpgsqlConnection("Server=cipg01;Port=5432;Database=GROUPD;User Id=postgres;Password=123456");
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("INSERT INTO t_orders115(c_userid, c_orderid, c_albumid, c_quantity, c_fname, c_lname, c_address, c_city, c_postalcode, c_state, c_country, c_phone, c_email, c_priority, c_shipdate, c_orderdate) VALUES(@c_customerid, @c_orderid, @c_albumid, @c_qty, @c_fname, @c_lname, @c_address, @c_city, @c_postalcode, @c_state, @c_country, @c_phone, @c_email, @c_priority, @c_shipdate, @c_orderdate) RETURNING c_orderid;", conn);
                command.Parameters.AddWithValue("@c_customerid", order.c_userid);
                command.Parameters.AddWithValue("@c_orderid", order.c_orderid);
                command.Parameters.AddWithValue("@c_albumid", order.c_albumid);
                command.Parameters.AddWithValue("@c_qty", order.c_qauntity);
                command.Parameters.AddWithValue("@c_fname", order.c_fname);
                command.Parameters.AddWithValue("@c_lname", order.c_lname);
                command.Parameters.AddWithValue("@c_address", order.c_address);
                command.Parameters.AddWithValue("@c_city", order.c_city);
                command.Parameters.AddWithValue("@c_postalcode", order.c_postalcode);
                command.Parameters.AddWithValue("@c_state", order.c_state);
                command.Parameters.AddWithValue("@c_country", order.c_country);
                command.Parameters.AddWithValue("@c_phone", order.c_phone);
                command.Parameters.AddWithValue("@c_email", order.c_email);
                command.Parameters.AddWithValue("@c_priority", order.c_priority);
                command.Parameters.AddWithValue("@c_shipdate", order.c_shipdate);
                command.Parameters.AddWithValue("@c_orderdate", order.c_orderdate);

                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    orderId = Convert.ToInt64(dr["c_orderid"]);
                }
                return orderId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Place order : " + ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }


       
    }
}