

namespace ServiceBusClient
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;

    internal class Program
    {
        private const String ServiceBusConnectionString = "Endpoint=sb://xxxxx.servicebus.windows.net/;SharedAccessKeyName=read;SharedAccessKey=xxxyyyzzz";
        private const String QueueName = "queue";
        private static IQueueClient queueClient;

        private static void Main(String[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            Console.WriteLine("======================================================");
            Console.WriteLine("TODAS LAS COLAS.");
            Console.WriteLine("======================================================");

            ReceivedMessages();

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        private static void ReceivedMessages()
        {
            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionReceived)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Mensaje: Secuencia:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionReceived(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Exception {exceptionReceivedEventArgs.Exception}.");
            ExceptionReceivedContext context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Detalle:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Path: {context.EntityPath}");
            Console.WriteLine($"- Acción: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
