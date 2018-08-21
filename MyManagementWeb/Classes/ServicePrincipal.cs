using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MyManagementWeb.Classes
{
    public static class ServicePrincipal
    {
        /// <summary>
        /// The variables below are standard Azure AD terms from our various samples
        /// We set these in the Azure Portal for this app for security and to make it easy to change (you can reuse this code in other apps this way)
        /// You can name each of these what you want as long as you keep all of this straight
        /// </summary>
        internal static string authority = ConfigurationManager.AppSettings["ida:Authority"];  // the AD Authority used for login.  For example: https://login.microsoftonline.com/myadnamehere.onmicrosoft.com 
        internal static string clientId = ConfigurationManager.AppSettings["ida:ClientId"]; // the Application ID of this app.  This is a guid you can get from the Advanced Settings of your Auth setup in the portal
        internal static string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"]; // the key you generate in Azure Active Directory for this application
        internal static string resource = ConfigurationManager.AppSettings["ida:Resource"]; // the Application ID of the app you are going to call.  This is a guid you can get from the Advanced Settings of your Auth setup for the targetapp in the portal
        internal static string LocalHost = ConfigurationManager.AppSettings["LocalHost"]; // the Application ID of the app you are going to call.  This is a guid you can get from the Advanced Settings of your Auth setup for the targetapp in the portal


        /// <summary>
        /// wrapper that passes the above variables
        /// </summary>
        /// <returns></returns>

        public static async Task<string> GetAzureToken()
        {
            string token = "";
         if(LocalHost == "true") { token = await GetLocalHostAzureToken(); }
            else { token = await GetMSIToken(); }

            return token;
        }

        public static async Task<string> GetLocalHostAzureToken()
        {
            var clientCredential = new ClientCredential(clientId, clientSecret);

            AuthenticationContext context = new AuthenticationContext(authority, false);
            AuthenticationResult authenticationResult = await context.AcquireTokenAsync(
                resource,  // the resource (app) we are going to access with the token
                clientCredential);  // the client credentials

            return authenticationResult.AccessToken;
        }

        internal static async Task<string> GetMSIToken()
        {
            string apiversion = "2017-09-01";
            string resource = "https://management.azure.com/";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Secret", Environment.GetEnvironmentVariable("MSI_SECRET"));
            var t = await client.GetAsync(String.Format("{0}/?resource={1}&api-version={2}", Environment.GetEnvironmentVariable("MSI_ENDPOINT"), resource, apiversion));

            return t.ToString();
        }


    }
}