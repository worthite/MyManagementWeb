using MyManagementWeb.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyManagementWeb.Controllers
{
    public class ManagedApplicationsController : Controller
    {
        private static string _vaultName = ConfigurationManager.AppSettings["vaultName"];
        private static string _subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];
        
        [System.Web.Http.HttpGet]  
        [System.Web.Http.ActionName("Index")]
        [System.Web.Http.Route("api/ManagedApplications/")]
        public ActionResult Index()
        {
            ManagedApplications MyApps = new ManagedApplications();

            var mylist = MyApps.ApplicationsInSubscription(_subscriptionId);

            return View(mylist);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("ResourceGroup")]
        [System.Web.Http.Route("api/ManagedApplications/ResourceGroup")]
        public ActionResult ResourceGroup(string ResourceGroup)
        {
            ManagedApplications MyApps = new ManagedApplications();

            var mylist = MyApps.ApplicationsInResourceGroup(_subscriptionId, ResourceGroup);

            return View(mylist);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("Id")]
        [System.Web.Http.Route("api/ManagedApplications/Id")]
        public ActionResult Id(string applicationId)
        {
            ManagedApplications MyApps = new ManagedApplications();

            var mylist = MyApps.ApplicationId(applicationId);

            return View(mylist);
        }

    }
}