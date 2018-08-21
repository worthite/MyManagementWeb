
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyManagementWeb.Classes
{
    public class MyApplications
    {

        public MyApplications()
        {
            Items = new List<ManagedApplication>();
        }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public List<ManagedApplication> Items { get; set; }

        public MyApplications Load(AzureApplications apps)
        {
            
            foreach(var app in apps.Value)
            {
                this.Items.Add(new ManagedApplication { Id = app.Id, Kind = app.Kind, Location = app.Location, Name = app.Name, Type = app.Type, Properties = app.Properties });
            }

            return this;
        }
    }

    public class Instance
    {
        public string managedResourceGroupId { get; set; }
        public string applicationDefinitionId { get; set; }
        public string publisherPackageId { get; set; }
        public Parameters parameters { get; set; }
        public Outputs outputs { get; set; }
        public string provisioningState { get; set; }
        public List<Authorization> authorizations { get; set; }
    }

    public class ManagedApplication
    {
        [JsonProperty("Instance")]
        public Instance Instance { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }




    }
}