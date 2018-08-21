using MyManagementWeb.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyManagementWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private static string _vaultName = ConfigurationManager.AppSettings["vaultName"];
        private static string _subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Applications()
        {
            ViewBag.Message = "Your List of applications.";

            ManagedApplications MyApps = new ManagedApplications();

            string mylist = MyApps.ApplicationsInSubscription(_subscriptionId).Result;
            ManagedApps app = JsonConvert.DeserializeObject<ManagedApps>(mylist);

            return View(app);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}