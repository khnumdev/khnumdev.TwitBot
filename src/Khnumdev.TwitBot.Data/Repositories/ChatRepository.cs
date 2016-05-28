namespace Khnumdev.TwitBot.Data.Repositories
{
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Model;
    using Newtonsoft.Json;
    using System.Configuration;
    using System.Threading.Tasks;

    public class ChatRepository : IChatRepository
    {
        const string CHAT_QUEUE_NAME = "chat";

        public async Task EnqeueChatAsync(QueueChat chat)
        {
            var serializedMessage = JsonConvert.SerializeObject(chat);
            var message = new CloudQueueMessage(serializedMessage);

            var queue = GetQueue();
            await queue.AddMessageAsync(message);
        }

        CloudQueue GetQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference(CHAT_QUEUE_NAME);

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            return queue;
        }
    }
}
