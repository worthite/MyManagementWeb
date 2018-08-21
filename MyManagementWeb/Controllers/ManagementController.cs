using Microsoft.AspNetCore.Http;
using MyManagementWeb.Classes;
using MyManagementWeb.Extensions;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyManagementWeb.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ManagementWebSocketManager _socketManager;
        private static string _vaultName = ConfigurationManager.AppSettings["vaultName"];
        private static string _subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];

        public ManagementController(ManagementWebSocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        [System.Web.Http.HttpPost]
        public async Task ManagedApplications()
        {
            ManagedApplicationData MyApps = new ManagedApplicationData();

            var mylist = await MyApps.ApplicationsInSubscription(_subscriptionId);

            await _socketManager.SendMessageToAllAsync("mylist");
        }

        [System.Web.Http.HttpPost]
        public async Task ManagedApplication(string applicationId)
        {
            ManagedApplicationData MyApps = new ManagedApplicationData();

            MyApplication mylist = await MyApps.ApplicationId(_subscriptionId, applicationId);
                                    
            await _socketManager.SendMessageToAllAsync(mylist.ToJson());
        }

    }
}
