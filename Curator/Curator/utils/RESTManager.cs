using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    public class RESTManager
    {
        IRestService restService;

        public RESTManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<Product>> GetProducts(int state)
        {
            return restService.GetProducts(state);
        }
        public List<Product> products()
        {
            return RestService.Products;
        }
        public Task<string> AcceptProducts(int state, int id)
        {
            return restService.AcceptProducts(state, id);
        }
        public Task<string> RejectProducts(int state, int id)
        {
            return restService.AcceptProducts(state, id);
        }
    }
}
