using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyManagementWeb.Classes
{
    public class AzureApplication
    {


            public Properties properties { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string kind { get; set; }
            public string location { get; set; }
        
            public class Properties
            {
                public string managedResourceGroupId { get; set; }
                public string applicationDefinitionId { get; set; }
                public string publisherPackageId { get; set; }
                public Parameters parameters { get; set; }
                public Outputs outputs { get; set; }
                public string provisioningState { get; set; }
                public List<Authorization> authorizations { get; set; }
            }

    }
}