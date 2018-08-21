using MyManagementWeb.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyManagementWeb.Controllers
{
    public class ManagedApplicationsController : ApiController
    {
        private static string _vaultName = ConfigurationManager.AppSettings["vaultName"];
        private static string _subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("api/ManagedApplications/Test")]
        public async Task<string> Test()
        {
            string t = await Task.Run(() => DateTime.UtcNow.ToLongTimeString());

            return t;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/ManagedApplications/")]
        public async Task<string> List()
        {
        
                ManagedApplications MyApps = new ManagedApplications();

            var mylist = await MyApps.ApplicationsInSubscription(_subscriptionId);

            return mylist;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/ManagedApplications/Id")]
        public async Task<string> Id(string applicationId)
        {
            ManagedApplications MyApps = new ManagedApplications();

            var mylist = await MyApps.ApplicationId(_subscriptionId,applicationId);

            return mylist;
        }

    }
}