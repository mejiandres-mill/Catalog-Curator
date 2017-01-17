using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Curator
{
    class RestService : IRestService
    {
        HttpClient client;

        public static List<Product> Products { get; private set; }

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            Products = new List<Product>();
            Products.Add(new Product() { name = "producto 1", image = "profilepic.png" });
        }
        

        public async Task<List<Product>> GetProducts(int state)
        {
            Products = new List<Product>();
            JObject data = new JObject();
            data.Add("state", state);
            var uri = new Uri(Constants.RestURL);
            PrepareClient();

            JObject sendContent = CreateMessage( Constants.SHOW_PRODS, data);
            var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");
            Debug.WriteLine(sendContent.ToString());
            try
            {
                var response = await client.PostAsync(uri, content);
                if(response.IsSuccessStatusCode)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    Debug.WriteLine("*_*_*_*_*_*_*__*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*");
                    Debug.WriteLine(sr.data);
                    Debug.WriteLine("*_*_*_*_*_*_*__*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*");
                    Products = JsonConvert.DeserializeObject<List<Product>>(sr.data.ToString());
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
            return Products;
        }
        public async Task<string> AcceptProducts(int state, int id)
        {
            var uri = new Uri(Constants.RestURL);
            JObject data = new JObject();
            data.Add("state", state);
            data.Add("idproduct", id);
            JObject sendContent = CreateMessage(Constants.ACCEPT_PROD, data);
            try
            {
                var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                Debug.WriteLine(result);
                if (response.IsSuccessStatusCode)
                {
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    return sr.data.ToString();
                }
                else
                {
                    return "FAIL";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine(e.Message);
                return "FAIL";
            }
        }

        public async Task<string> RejectProducts(int state, int id)
        {
            var uri = new Uri(Constants.RestURL);
            JObject data = new JObject();
            data.Add("state", state);
            data.Add("idproduct", id);
            JObject sendContent = CreateMessage(Constants.REJECT_PROD, data);
            try
            {
                var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                Debug.WriteLine(result);
                if (response.IsSuccessStatusCode)
                {
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    return sr.data.ToString();
                }
                else
                {
                    return "FAIL";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine(e.Message);
                return "FAIL";
            }
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

        private void PrepareClient()
        {            
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "11614066f1b101f695bf2479656da628");
        }
    }
}
