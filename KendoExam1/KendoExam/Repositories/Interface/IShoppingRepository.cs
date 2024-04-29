using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositories.Models;

namespace Repositories.Repository
{
    public interface IShoppingRepository
    {List<Album> GetAlbums();

        bool AddToCart(Shopping shop);

        List<Shopping> GetAllCart(int userid);

        bool RemoveCart(int cartId);

        long Placeorder(CheckOut order);
 }
}