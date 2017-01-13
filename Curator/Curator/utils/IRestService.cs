using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    interface IRestService
    {
        Task<List<Product>> GetProducts();
        Task<bool> AcceptProduct(int idproduct);
        Task<bool> RejectProduct(int idproduct);
    }
}
