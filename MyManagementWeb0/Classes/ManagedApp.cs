
using System.Collections.Generic;

public class Properties
{
    public string managedResourceGroupId { get; set; }
    public string applicationDefinitionId { get; set; }
    public string publisherPackageId { get; set; }

    public string provisioningState { get; set; }
}

public class Value
{
    public Properties properties { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string kind { get; set; }
    public string location { get; set; }
}

public class ManagedApps
{
    public List<Value> value { get; set; }
}