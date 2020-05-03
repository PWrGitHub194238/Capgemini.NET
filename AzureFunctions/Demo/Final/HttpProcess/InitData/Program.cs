using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace InitData
{
    public class Program
    {
        async static Task Main()
        {
            CloudBlobContainer container = GetBlobContainer();
            CosmosClient cosmosClient = GetCosmosDbClient();

            foreach (var invoiceDoc in Directory.GetFiles("BlobFiles", "*.pdf"))
            {
                await CreateDocument(cosmosClient, invoiceDoc);
                UploadInvoice(container, invoiceDoc);
            }

            cosmosClient.Dispose();
        }

        private async static Task CreateDocument(CosmosClient cosmosClient, string invoiceDoc)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            var dbContainer = cosmosClient.GetDatabase("invoice-db").GetContainer("invoices");

            await dbContainer.CreateItemAsync(new Invoice
            {
                ID = Path.GetFileNameWithoutExtension(invoiceDoc),
                MailTo = configuration["mailTo"],
                PhoneTo = configuration["phoneTo"]
            });
        }

        private static CosmosClient GetCosmosDbClient()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            CosmosClient cosmosClient = new CosmosClient(configuration.GetConnectionString("CosmosDB"));
            return cosmosClient;
        }

        private static CloudBlobContainer GetBlobContainer()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("StorageAccount"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(configuration.GetSection("StorageAccount")["BlobContainerName"]);
            return container;
        }

        private static void UploadInvoice(CloudBlobContainer container, string invoiceDoc)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.GetFileNameWithoutExtension(invoiceDoc));

            using (Stream fileStream = File.OpenRead(invoiceDoc))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}
