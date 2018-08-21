
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


public partial class Value
{
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

public class AzureApplications
{
    [JsonProperty("value")]
    public Value[] Value { get; set; }
}

public partial class Properties
{
    [JsonProperty("managedResourceGroupId")]
    public string ManagedResourceGroupId { get; set; }

    [JsonProperty("applicationDefinitionId")]
    public string ApplicationDefinitionId { get; set; }

    [JsonProperty("publisherPackageId")]
    public string PublisherPackageId { get; set; }

    [JsonProperty("parameters")]
    public Parameters Parameters { get; set; }

    [JsonProperty("provisioningState")]
    public string ProvisioningState { get; set; }

    [JsonProperty("outputs", NullValueHandling = NullValueHandling.Ignore)]
    public Outputs Outputs { get; set; }

    [JsonProperty("authorizations", NullValueHandling = NullValueHandling.Ignore)]
    public Authorization[] Authorizations { get; set; }
}

public partial class Authorization
{
    [JsonProperty("principalId")]
    public Guid PrincipalId { get; set; }

    [JsonProperty("roleDefinitionId")]
    public Guid RoleDefinitionId { get; set; }
}

public partial class Outputs
{
}

public partial class Parameters
{
    [JsonProperty("logAnalyticsLocation")]
    public AddSqlServer LogAnalyticsLocation { get; set; }

    [JsonProperty("masterSPNID")]
    public AddSqlServer MasterSpnid { get; set; }

    [JsonProperty("tenantID")]
    public AddSqlServer TenantId { get; set; }

    [JsonProperty("roleNameGuid")]
    public AddSqlServer RoleNameGuid { get; set; }

    [JsonProperty("addSQLServer", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer AddSqlServer { get; set; }

    [JsonProperty("sqlConnectionName", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer SqlConnectionName { get; set; }

    [JsonProperty("sqlServerName", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer SqlServerName { get; set; }

    [JsonProperty("sqlDatabaseName", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer SqlDatabaseName { get; set; }

    [JsonProperty("sqlServerLogin", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer SqlServerLogin { get; set; }

    [JsonProperty("sqlServerLoginPassword", NullValueHandling = NullValueHandling.Ignore)]
    public ArtifactsSasToken SqlServerLoginPassword { get; set; }

    [JsonProperty("artifactsUri", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer ArtifactsUri { get; set; }

    [JsonProperty("artifactsSasToken", NullValueHandling = NullValueHandling.Ignore)]
    public ArtifactsSasToken ArtifactsSasToken { get; set; }

    [JsonProperty("storagekey", NullValueHandling = NullValueHandling.Ignore)]
    public ArtifactsSasToken Storagekey { get; set; }

    [JsonProperty("dnsHostName", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer DnsHostName { get; set; }

    [JsonProperty("branch", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer Branch { get; set; }

    [JsonProperty("repository", NullValueHandling = NullValueHandling.Ignore)]
    public AddSqlServer Repository { get; set; }
}

public partial class AddSqlServer
{


    [JsonProperty("value")]
    public string Value { get; set; }
}

public partial class ArtifactsSasToken
{
    [JsonProperty("type")]
    public ArtifactsSasTokenType Type { get; set; }
}

public enum ArtifactsSasTokenType { SecureString };