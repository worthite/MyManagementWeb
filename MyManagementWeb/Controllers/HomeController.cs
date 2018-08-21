using MyManagementWeb.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyManagementWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private static string _vaultName = ConfigurationManager.AppSettings["vaultName"];
        private static string _subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];

        [System.Web.Http.Authorize]
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Applications()
        {
            ViewBag.Message = "Your application List page.";

            ManagedApplicationData MyApps = new ManagedApplicationData();
          
            var mylist = await MyApps.ApplicationsInSubscription(_subscriptionId);
   
            return View(mylist.Items);
        }

        public async Task<ActionResult> Application(String ApplicationId)
        {
            ViewBag.Message = "Your application List page.";

            ManagedApplicationData MyApps = new ManagedApplicationData();

            var mylist = await MyApps.ApplicationId(_subscriptionId, ApplicationId);


            return View(mylist);
        }

        public async Task<ActionResult> ManagedIdentities(string ResourceGroup)
        {
            ViewBag.Message = "Your Managed Identities List page.";

            ManagedApplicationData MyApps = new ManagedApplicationData();

            var mylist = await MyApps.ManagedIdentitiesInSubscription(_subscriptionId, ResourceGroup);

            return View(mylist);
        }


    }
}