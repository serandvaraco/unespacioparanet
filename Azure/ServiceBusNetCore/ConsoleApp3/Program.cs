
namespace ServiceBusServer
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;
    using Newtonsoft.Json;

    internal class Program
    {

        private static void Main(String[] args) => MainAsync().GetAwaiter().GetResult();
        private static async Task MainAsync()
        {
            IQueueClient queue = new QueueClient("Endpoint=sb://xxxxxxxx.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=xxxyyyzzz=", "queue");

            String[] countries = new[] { "Colombia", "Brazil", "Mexico", "Argentina", "Alemania", "Panama", "Chile", "Costa Rica", "España", "China", "Japon" };
            Random rnd = new Random(DateTime.Now.Millisecond);

            while (true)
            {
                String objectSerializer = JsonConvert.SerializeObject(new { message = $"Hello {countries[rnd.Next(10)]}" });
                Byte[] objectBytes = Encoding.UTF8.GetBytes(objectSerializer);

                await queue.SendAsync(new Message
                {
                    Body = objectBytes,
                    ContentType = Encoding.UTF8.EncodingName,
                    PartitionKey = Guid.NewGuid().ToString()
                });

                await Task.Delay(500);

            }

        }
    }
}


