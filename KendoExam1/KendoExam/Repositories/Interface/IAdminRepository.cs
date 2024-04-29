using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Repository
{
    public interface IAdminRepository
    {
         List<Album> GetAlbums();

        Album GetDetails(int AlbumId);

        bool AddAlbum(Album album);

        bool EditAlbum(Album album);

        bool DeleteAlbum(int AlbumId);

        List<AlbumType> GetAllAlbumType();
    }
}