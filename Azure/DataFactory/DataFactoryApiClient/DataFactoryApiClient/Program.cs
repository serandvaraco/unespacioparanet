using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFactoryApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecutePïpeline();
            Console.WriteLine("Pulse una tecla pára continuar ");
            Console.ReadKey();
        }

        private static async void ExecutePïpeline()
        {
            var dataFactory = new DataFactoryManagement();

            dataFactory.DataFactoryMonitoringEvent += DataFactory_DataFactoryMonitoringEvent;
            await dataFactory.ExecutePipeline("CopyPipelineStorageSql");
        }

        private static void DataFactory_DataFactoryMonitoringEvent(object sender, Microsoft.Azure.Management.DataFactory.Models.PipelineRun e)
        {
            Console.WriteLine($"{e.Message}, {e.Status}, {e.RunStart.GetValueOrDefault().ToLongDateString()}, {e.RunId}");
        }
    }
}
