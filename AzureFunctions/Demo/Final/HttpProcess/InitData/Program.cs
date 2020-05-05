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
        // Creates document entries for the Azure Cosmos DB along with Azure Blob Storage based on predefined set of documents in the 'BlobFiles' directory.
        async static Task Main()
        {
            // Build client objects both for Azure Cosmos DB and Azure Blob Storage 
            CloudBlobContainer container = GetBlobContainer();
            CosmosClient cosmosClient = GetCosmosDbClient();

            // For each '*.pdf' file containing an invoice
            // put a new document inside the 'invoice-db' Cosmos DB within the 'invoices' document container with the details like:
            // - invoice ID,
            // - customer email
            // - customer phone number.
            // Then each file will be uploaded to Azure Blob Storage.
            foreach (var invoiceDoc in Directory.GetFiles("BlobFiles", "*.pdf"))
            {
                await CreateDocument(cosmosClient, invoiceDoc);
                UploadInvoice(container, invoiceDoc);
            }

            cosmosClient.Dispose();
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

        private async static Task CreateDocument(CosmosClient cosmosClient, string invoiceDoc)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            var dbContainer = cosmosClient.GetDatabase("invoice-db").GetContainer("invoices");

            await dbContainer.CreateItemAsync(new Invoice
            {
                ID = Path.GetFileNameWithoutExtension(invoiceDoc),
                MailTo = configuration["mailTo"],   // comes from User Secrets
                PhoneTo = configuration["phoneTo"]  // comes from User Secrets
            });
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
