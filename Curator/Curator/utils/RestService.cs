using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Diagnostics;

namespace Curator
{
    class RestService : IRestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public Task<bool> AcceptProduct(int idproduct)
        {
            return null;
        }

        public async Task<List<Product>> GetProducts()
        {
            List<Product> prods = new List<Product>();
            var uri = new Uri(Constants.RestURL);

            JObject sendContent = CreateMessage(Constants.SHOW_PRODS, null);
            var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(uri, content);
                if(response.IsSuccessStatusCode)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    prods = JsonConvert.DeserializeObject<List<Product>>(sr.data);
                }
                else
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
            return prods;
        }

        public Task<bool> RejectProduct(int idproduct)
        {
            return null;
        }

        private JObject CreateMessage(int operation, JObject data)
        {
            JObject message = new JObject();
            JObject control = new JObject();
            control.Add("operation", operation);
            message.Add("control", control);

            if (data != null)
                message.Add("data", data);

            return message;
        }
    }
}
