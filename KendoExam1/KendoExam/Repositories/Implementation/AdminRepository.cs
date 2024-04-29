using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repositories.Models;
using Repositories.Repository;

namespace Repositories.Repository
{
    public class AdminRepository: CommonRepository, IAdminRepository
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AdminRepository(IConfiguration myConfiguration, IHttpContextAccessor httpContextAccessor) : base(myConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        bool IAdminRepository.AddAlbum(Album album)
        {
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("INSERT INTO t_albums115(c_image, c_type, c_artist, c_title, c_price) VALUES (@image, @type, @artist, @title, @price);", conn);
                command.Parameters.AddWithValue("image", album.c_image);
                command.Parameters.AddWithValue("type", album.c_type);
                command.Parameters.AddWithValue("artist", album.c_artist);
                command.Parameters.AddWithValue("title", album.c_title);
                command.Parameters.AddWithValue("price", album.c_price);
                var result = command.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding the album data.." + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        bool IAdminRepository.DeleteAlbum(int AlbumId)
        {
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("DELETE FROM t_albums115 WHERE c_albumid=@id;", conn);
                command.Parameters.AddWithValue("id", AlbumId);
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Updating the album data.." + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        bool IAdminRepository.EditAlbum(Album album)
        {
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("UPDATE t_albums115 SET c_image=@image, c_type=@type, c_artist=@artist, c_title=@title, c_price=@price WHERE c_albumid = @id;", conn);
                command.Parameters.AddWithValue("image", album.c_image);
                command.Parameters.AddWithValue("type", album.c_type);
                command.Parameters.AddWithValue("artist", album.c_artist);
                command.Parameters.AddWithValue("title", album.c_title);
                command.Parameters.AddWithValue("price", album.c_price);
                command.Parameters.AddWithValue("id" , album.c_albumid);
                var result = command.ExecuteNonQuery();
                return result > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Updating the album data.." + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        List<Album> IAdminRepository.GetAlbums()
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

        List<AlbumType> IAdminRepository.GetAllAlbumType()
        {
            List<AlbumType> types = new List<AlbumType>();
            try
            {
                conn.Open();
                var command = new NpgsqlCommand("SELECT c_id, c_type FROM t_albumtype115;", conn);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var type = new AlbumType
                    {
                        c_id = reader.GetInt32(0),
                        c_type = reader.GetString(1)
                    };
                    types.Add(type);
                }
                return types;
            }
            catch (Exception ex)
            {
                Console.Write("Error While Fetching the Album Type ... " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        Album IAdminRepository.GetDetails(int AlbumId)
        {
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
                    return album;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while Fetching Individual Data.." + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}