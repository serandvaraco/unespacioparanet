using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Threading.Tasks;

namespace DataFactoryApiClient
{
    class StoreCredentials
    {
        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
        public string AuthenticationKey { get; set; }
        public string SuscriptionId { get; set; }
        public string ResourceGroupName { get; set; }
        public string FactoryName { get; set; }


    }
    public sealed class DataFactoryManagement
    {

        public event EventHandler<PipelineRun> DataFactoryMonitoringEvent;

        private void OnDataFactoryMonitoringEvent(PipelineRun e)
            => this.DataFactoryMonitoringEvent?.Invoke(this, e);

        private readonly StoreCredentials storeCredentials;
        public DataFactoryManagement()
        {
            storeCredentials = new StoreCredentials();
            initCredentials();
        }

        private void initCredentials()
        {
            storeCredentials.TenantId = "#######";
            storeCredentials.ResourceGroupName = "GitHubDataFactories";
            storeCredentials.SuscriptionId = "#####";
            storeCredentials.FactoryName = "serandvaracofactories";
            storeCredentials.ApplicationId = "########";
            storeCredentials.AuthenticationKey = "#########";

        }

        public async Task ExecutePipeline(string pipelineName)
        {

            try
            {

                DataFactoryManagementClient dataFactoryClient = await AuthenticationAzureServices();

                var createRunResponse = await dataFactoryClient.Pipelines.CreateRunAsync(
                                                    storeCredentials.ResourceGroupName,
                                                    storeCredentials.FactoryName, pipelineName);



                PipelineRun pipelineRun;
                while (true)
                {
                    pipelineRun = dataFactoryClient.PipelineRuns.Get(storeCredentials.ResourceGroupName,
                        storeCredentials.FactoryName,
                        createRunResponse.RunId);

                    OnDataFactoryMonitoringEvent(pipelineRun);

                    if (pipelineRun.Status != "InProgress")
                        break;
                }

                OnDataFactoryMonitoringEvent(pipelineRun);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private async Task<DataFactoryManagementClient> AuthenticationAzureServices()
        {
            try
            {
                var context = new AuthenticationContext("https://login.windows.net/" + storeCredentials.TenantId);
                ClientCredential clientCredential = new ClientCredential(storeCredentials.ApplicationId, storeCredentials.AuthenticationKey);
                AuthenticationResult authenticationResult = await context.AcquireTokenAsync("https://management.azure.com/", clientCredential);
                ServiceClientCredentials cred = new TokenCredentials(authenticationResult.AccessToken);

                return new DataFactoryManagementClient(cred) { SubscriptionId = storeCredentials.SuscriptionId };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
