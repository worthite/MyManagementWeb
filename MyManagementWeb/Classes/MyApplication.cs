using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyManagementWeb.Classes
{
    public class MyApplication
    {
            public Properties Instance { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string kind { get; set; }
            public string location { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public MyApplication Load(AzureApplication app)
        {
            this.Instance = new Properties();
            this.id = app?.id;
            this.kind = app?.kind;
            this.location = app?.location;
            this.name = app?.name;
            this.type = app?.type;

            if (app.properties != null)
            {
                this.Instance.applicationDefinitionId = app.properties.applicationDefinitionId;
                this.Instance.managedResourceGroupId = app.properties.managedResourceGroupId;
                this.Instance.provisioningState = app.properties.provisioningState;
                this.Instance.publisherPackageId = app.properties.publisherPackageId;
                this.Instance.parameters = app.properties.parameters;
                this.Instance.outputs = app.properties.outputs;
            } else
            {
                this.Instance.provisioningState = "Not Found";
            }
            return this;
        }

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