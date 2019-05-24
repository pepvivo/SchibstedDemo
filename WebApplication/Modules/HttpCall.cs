using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Managers;

namespace WebApplication.Modules
{
    /// <summary>
    /// Standart HttpClient caller
    /// </summary>
    public class HttpCall 
    {
        public const string GET = "GET";
        public const string POST = "POST";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";

        public virtual async Task<HttpResponseMessage> Exec(string apiUrl, string method)
        {
            return await Exec<object>(apiUrl, method, null);
        }

        public virtual async Task<HttpResponseMessage> Exec<T>(string apiUrl, string method, T obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var byteArray = Encoding.ASCII.GetBytes(SessionManager.Instance.CurrentUser.ToAutentification());
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                string userContent;
                byte[] buffer;
                ByteArrayContent byteContent;

                switch (method)
                {
                    case GET:
                        return await client.GetAsync(apiUrl);

                    case POST:
                        //Serialize User
                        userContent = JsonConvert.SerializeObject(obj);
                        //Serialize string
                        userContent = JsonConvert.SerializeObject(userContent);
                        buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                        byteContent = new ByteArrayContent(buffer);

                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        return await client.PostAsync(apiUrl, byteContent);

                    case PUT:
                        //Serialize User
                        userContent = JsonConvert.SerializeObject(obj);
                        //Serialize string
                        userContent = JsonConvert.SerializeObject(userContent);
                        buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                        byteContent = new ByteArrayContent(buffer);

                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        return await client.PutAsync(apiUrl, byteContent);

                    case DELETE:
                        return await client.DeleteAsync(apiUrl);

                    default:
                        throw new ArgumentException();
                }
            }

        }

    }
}