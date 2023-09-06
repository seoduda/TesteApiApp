using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TesteApiApp.Entities;

namespace TesteApiApp.Service
{
    internal class UserService : IDisposable
    {
        private const string BASE_API_URL = "https://jsonplaceholder.typicode.com/";


        public async Task<List<User>> GetUsers()
        {
            var options = new RestClientOptions(BASE_API_URL)
            {
                MaxTimeout = 3000000,
            };

            var client = new RestClient(options);
            var request = new RestRequest("/users", Method.Get);


            try
            {
                
                var response = await client.GetAsync(request);
                
                if (response.StatusCode == HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<List<User>>(response.Content);
                else
                {
                    return new List<User>();
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Todo>> GetUserTodo(int userId)
        {
            var options = new RestClientOptions(BASE_API_URL)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            string endpoint = $"/users/{userId}/todo";
            var request = new RestRequest(endpoint, Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<List<Todo>>(response.Content);
            else
            {
                return null;
            }
        }
    }
}