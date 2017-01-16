using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    public interface IRestService
    {
        Task<List<Product>> GetProducts(int state);
        Task<string> AcceptProducts(int state, int id);
        Task<string> RejectProducts(int state, int id);
    }
}
