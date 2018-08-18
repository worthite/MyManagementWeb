using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MyManagementWeb.Classes
{
    public class ManagedApplications
    {


        public async Task<string> ApplicationsInSubscription(string SubscriptionId)
        {
            string version = "2017-09-01";
            string azureResource = "https://management.azure.com/";

            var azureToken = GetToken(azureResource, version);

            return await ManagementApplicationList(SubscriptionId);
        }
        public async Task<string> ApplicationId(string applicationId)
        {
            string version = "2017-09-01";
            string azureResource = "https://management.azure.com/";

            var azureToken = GetToken(azureResource, version);

            return await ManagementApplicationId(applicationId);
        }

        public async Task<string> ApplicationsInResourceGroup(string SubscriptionId, string ResourceGroup)
        {
            string version = "2017-09-01";
            string azureResource = "https://management.azure.com/";

            var azureToken = GetToken(azureResource, version);

            return await ManagementApplicationResourceGroup(SubscriptionId, ResourceGroup);
        }

        internal async Task<string> ManagementApplicationId(string applicationId)
        {
            string version = "2017-09-01";
            string azureResource = "https://management.azure.com/";

            var azureToken = GetToken(azureResource, version);

            string apiBaseUri = $"GET https://management.azure.com/{applicationId}?api-version=2017-09-01"; 
            string result = "";
            

            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + azureToken);

                //make request
                HttpResponseMessage response = await client.GetAsync(apiBaseUri);

                result = await response.Content.ReadAsStringAsync();
                
            }
            
            return result;
        }
        internal async Task<string> ManagementApplicationList(string subscriptionId)
        {
            string version = "2017-09-01";
            string azureResource = "https://management.azure.com/";

            var azureToken = GetToken(azureResource, version);

            string apiBaseUri = $"https://management.azure.com/subscriptions/{subscriptionId}/providers/Microsoft.Solutions/applications?api-version=2017-09-01";
            string result = "";


            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + azureToken);

                //make request
                HttpResponseMessage response = await client.GetAsync(apiBaseUri);

                result = await response.Content.ReadAsStringAsync();

            }

            return result;
        }

        internal async Task<string> ManagementApplicationResourceGroup(string subscriptionId, string resourceGroupName)
        {
            string version = "2017-09-01";
            string azureResource = "https://management.azure.com/";

            var azureToken = GetToken(azureResource, version);

            string apiBaseUri = $"GET https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Solutions/applications?api-version=2017-09-01";
            string result = "";


            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + azureToken);

                //make request
                HttpResponseMessage response = await client.GetAsync(apiBaseUri);

                result = await response.Content.ReadAsStringAsync();

            }

            return result;
        }


        internal static HttpResponseMessage GetToken(string resource, string apiversion)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Secret", Environment.GetEnvironmentVariable("MSI_SECRET"));
            return client.GetAsync(String.Format("{0}/?resource={1}&api-version={2}", Environment.GetEnvironmentVariable("MSI_ENDPOINT"), resource, apiversion)).Result;
        }

        static async Task<string> GetRequest(string token, string apiBaseUri, string requestPath)
        {
            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //make request
                HttpResponseMessage response = await client.GetAsync(requestPath);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
        }
    

    }
}