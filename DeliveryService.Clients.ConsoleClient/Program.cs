using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Clients.ConsoleClient {

   //This console application is just for testing purposes as an example of a call to the routes API.
   
   class Program {

      public static IConfigurationRoot Configuration;

      static void Main(string[] args) {

         Console.OutputEncoding = Encoding.UTF8;
         string environment = "Development";
         //string environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

         if (String.IsNullOrWhiteSpace(environment))
            throw new ArgumentNullException("Environment not found in NETCORE_ENVIRONMENT");

         // Set up configuration sources.
         var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(AppContext.BaseDirectory))
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile($"appsettings.{environment}.json", optional: true);
         Configuration = builder.Build();

         //var configs = Configuration.GetSection("AppSettings").AsEnumerable(makePathsRelative: true);

         try {

            //The route http://localhost:5001/api/routes is protected for Admins Only
            //User filipe is Admin and Bob is a normal user

            string url = "http://localhost:5001/api/routes";
            string api = "routesApi";
            string user1 = "filipe", user2 = "bob";
            string password = "password";

            var tokenFilipe = GetToken("http://localhost:5000/", "client", "secret", user1, password, api).Result;
            if (tokenFilipe.IsError)
               Console.WriteLine(tokenFilipe.Error);
            CallApi(url, tokenFilipe);

            var tokenBob = GetToken("http://localhost:5000/", "client", "secret", user2, password, api).Result;
            if (tokenBob.IsError)
               Console.WriteLine(tokenBob.Error);
            CallApi(url, tokenBob);

         } catch (Exception ex) {
            Console.WriteLine(ex.Message);
         }

         Console.Read();

      }

      private static async void CallApi(string url, TokenResponse tokenResponse) {
         // call api
         var client = new HttpClient();
         client.SetBearerToken(tokenResponse.AccessToken);

         var response = await client.GetAsync(url);
         if (!response.IsSuccessStatusCode) {
            Console.WriteLine(response.StatusCode + ":" + response.RequestMessage);
         } else {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
         }
      }

      private static async Task<TokenResponse> GetToken(string url, string clientName, string secret, string user, string password, string apiName) {
         var disco = await DiscoveryClient.GetAsync(url);
         if (disco.IsError) {
            throw new Exception(disco.Error);
         }
         var tokenClient = new TokenClient(disco.TokenEndpoint, clientName, secret);
         var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(user, password, apiName);
         return tokenResponse;
      }
   }
}
