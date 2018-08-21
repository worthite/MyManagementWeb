using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyManagementWeb.Classes
{
    public class ManagedApplicationData
    {


        public async Task<MyApplications> ApplicationsInSubscription(string SubscriptionId)
        {
            string azureToken = await ServicePrincipal.GetAzureToken();
            MyApplications theseApps = new MyApplications();

            var ApplicationsDeployed = await ManagementApplicationList(SubscriptionId, azureToken);

                theseApps.Load(JsonConvert.DeserializeObject<AzureApplications>(ApplicationsDeployed));

            foreach (var DeployedApp in theseApps.Items)
            {
                    var appX = await ApplicationId(SubscriptionId, DeployedApp.Id);

              
                if (DeployedApp.Instance == null) DeployedApp.Instance = new Instance();

                if (appX.Instance != null)
                {
                
                    DeployedApp.Instance.applicationDefinitionId = appX.Instance.applicationDefinitionId;
                    DeployedApp.Instance.authorizations = appX.Instance.authorizations;
                    DeployedApp.Instance.managedResourceGroupId = appX.Instance.managedResourceGroupId;
                    DeployedApp.Instance.publisherPackageId = appX.Instance.publisherPackageId;
                    DeployedApp.Instance.parameters = appX.Instance.parameters;
                    DeployedApp.Instance.outputs = appX.Instance.outputs;
                    DeployedApp.Instance.provisioningState = appX.Instance.provisioningState;

                } else
                {
                    DeployedApp.Instance.provisioningState = "Not Found";
                }
            }

            return theseApps;
        }

        public async Task<string> ManagedIdentitiesInSubscription(string SubscriptionId,string ResourceGroup)
        {
            string azureToken = await ServicePrincipal.GetAzureToken();
          
            var theseIdentities = await ManagedIdentityList(SubscriptionId, ResourceGroup, azureToken);

            
            return theseIdentities;
        }


        public async Task<MyApplication> ApplicationId(string SubscriptionId,string applicationId)
        {
            MyApplication thisApp = new MyApplication();
            string azureToken = await ServicePrincipal.GetAzureToken();

            string app = await ManagementApplicationId(applicationId, azureToken);

            thisApp.Load(JsonConvert.DeserializeObject<AzureApplication>(app));

            return thisApp;
        }


        internal async Task<string> ManagementApplicationId(string applicationId,string azureToken)
        {
          
            string apiBaseUri = $"https://management.azure.com{applicationId}?api-version=2017-09-01"; 
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

        internal async Task<string> ManagementApplicationList(string subscriptionId, string azureToken)
        { 
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

        internal async Task<string> ManagedIdentityList(string subscriptionId,string ResourceGroup, string azureToken)
        {
            string apiBaseUri = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{ResourceGroup}/providers/Microsoft.ManagedIdentity/userAssignedIdentities?api-version=2015-08-31-preview";
                        
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


        internal async Task<string> ManagementApplicationResourceGroup(string subscriptionId, string resourceGroupName,string azureToken)
        {
                     

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