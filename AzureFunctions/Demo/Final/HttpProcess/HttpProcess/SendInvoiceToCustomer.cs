using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using Twilio.Rest.Api.V2010.Account;
using InitData;
using Twilio.Types;

namespace HttpProcess
{
    public static class SendInvoiceToCustomer
    {
        /* Entry point for the function
        *
        * Every static method with that attribute will be discover during program's startup.
        *
        * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-dotnet-class-library
        */
        [FunctionName("SendInvoiceToCustomer")]
        /*  Attribute that describes meta-data for the return type.
        *
        * In this particular example the QueueAttribute was used which is pointing to the 'invoices-queue' created inside the Azure Portal.
        * Providing that the connection string to the Azure Queue inside the Azure Storage is valid,
        * it will treat any returned value from this method as an input value for described Azure Queue
        * and the given IActionResult will be added as a new element for the 'invoices-queue' queue (in a JSON format).
        *
        * Connection value defines a key in 'local.settings.json' file that stores all settings for the Azure Function locally.
        * From that file the value from the key-value pair with the given key will be used as a connection string to the Azure Queue.
        *
        * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk/blob/master/src/Microsoft.Azure.WebJobs.Extensions.Storage/Queues/QueueAttribute.cs
        * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-storage-queue-output
        * You can read more about attribute targets here: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/#attribute-targets
        */
        [return: Queue("invoices-queue", Connection = "AzureWebJobsStorage")]
        public static async Task<IActionResult> Run(
            /* One of many available attributes that describes by which source/event the Azure Function will be triggered
            *
            * Attribute for triggering the method upon incoming HTTP request with an optionally defined Route.
            * Here we can configure an AuthorizationLevel (does the sender of the request is required to include 
            * a 'x-functions-key' header with a system/master API key), a set of HTTP methods to be accepted
            * and a Route parameter that defines our HTTP route with optional parameters.
            * If not provided the FunctionName will be used.
            *
            * This route defines a route parameter 'guid' that will be mapped to an extra 'string guid' input parameter
            * and also will be used by other Azure specific input binding attributes.
            * It will accept on HTTP POST requests and challenges user for a system API key defined by an Azure Function App inside Azure
            * before the request can be processed. Key can be either include as a query parameter (?code=<API_KEY>)
            * or included in a 'x-functions-key' header.
            *
            * Returns HttpRequest object with all data about the request that triggers the function.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/master/src/WebJobs.Extensions.Http/HttpTriggerAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-http-webhook
            */
            [HttpTrigger(
                AuthorizationLevel.Function, "POST", Route = "Send/{guid}")] HttpRequest req,
            /* One of many available attributes that describes an input binding for the parameter.
            *
            * This attribute defines the input binding in the way that invoice parameter
            * will be fed by any Invoice documents found in the defined CosmosDB No-SQL document collection 
            * of the given name COLECTION_NAME returned from the database with the given name DATABASE_NAME.
            *
            * Providing that the given ConnectionString is valid for the given database, the selected document collection
            * will be queried for the document with the given Id to be equal to the given guid string parameter.
            *
            * The definition of the PartitionKey is defined during Cosmos DB collection creation
            * and in this case PartitionKey is defined as the Id field of the document so value of both parameters are the same.
            *
            * ConnectionStringSetting value defines a key in 'local.settings.json' file that stores all settings for the Azure Function locally.
            * From that file the value from the key-value pair with the given key will be used as a connection string to the database.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/dev/src/WebJobs.Extensions.CosmosDB/CosmosDBAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-cosmosdb-v2-input
            */
            [CosmosDB(
                databaseName: "invoice-db",
                collectionName: "invoices",
                ConnectionStringSetting = "CosmosDBConnectionString",
                Id = "{guid}",
                PartitionKey = "{guid}")] Invoice invoice,
            /* One of many available attributes that describes an input binding for the parameter.
            *
            * This attribute defines the input binding in the way that invoiceStream parameter of type Stream
            * will be fed with the data stream of the specific file that is stored inside the given Azure Blob Storage.
            *
            * Providing that the given Connection is valid for the Azure Blob Storage, it grants a read permissions
            * and also inside the Azure Blob Container named 'invoices' exists a document with name given by the 'guid'
            * provided by the HTTP Trigger (and binded to the string parameter 'guid'), the new Stream for that file will be returned
            * as a value for the invoiceStream parameter.
            *
            * Connection value defines a key in 'local.settings.json' file that stores all settings for the Azure Function locally.
            * From that file the value from the key-value pair with the given key will be used as a connection string to the Azure Blob Storage.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk/blob/master/src/Microsoft.Azure.WebJobs.Extensions.Storage/Blobs/BlobAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-storage-blob-input
            */
            [Blob(
                "invoices/{guid}",
                FileAccess.Read,
                Connection = "AzureWebJobsStorage")] Stream invoiceStream,
            /* One of many available attributes that describes an output binding for the parameter.
            *
            * This attribute defines a IAsyncCollector for objects of type SendGridMessage - an asynchronous collection of emails to be send.
            *
            * Providing that the given ApiKey is valid, any email message of type SendGridMessage that was added during the method execution
            * by calling the AddAsync(SendGridMessage) method will be passed to the SendGrid service which will sent email messages
            * with a given sender/receiver/subject/body/attachments defined in each of the added SendGridMessage objects.
            *
            * ApiKey value defines a key in 'local.settings.json' file that stores all settings for the Azure Function locally.
            * From that file the value from the key-value pair with the given key will be used as a API Key for SendGrid service
            * to accept any request that will be sent over.
            *
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/dev/src/WebJobs.Extensions.SendGrid/SendGridAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-sendgrid
            */
            [SendGrid(
                ApiKey = "SendGridApiKey")] IAsyncCollector<SendGridMessage> mailCollector,
            /* One of many available attributes that describes an output binding for the parameter.
            *
            * This attribute defines a IAsyncCollector for objects of type CreateMessageOptions - an asynchronous collection 
            * of text messages to be send form the given From phone number (one assigned by Twilio for usage).
            *
            * Providing that all account parameters are valid, upon this method execution all text messages
            * added to the IAsyncCollector by calling AddAsync(CreateMessageOptions) will be sent over the mobile network.
            *
            * Both AccountSidSetting and AuthTokenSetting values define a key name in 'local.settings.json' file that stores all settings 
            * for the Azure Function locally. From that file the values from the key-value pairs with the given keys will be used 
            * as account identifier and matching API key authorizing incoming requests.
            * 
            * Source code for the attribute: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/dev/src/WebJobs.Extensions.Twilio/TwilioSMSAttribute.cs
            * You can read more here: https://docs.microsoft.com/pl-pl/azure/azure-functions/functions-bindings-twilio
            */
            [TwilioSms(
                AccountSidSetting = "TwilioAccountSid",
                AuthTokenSetting = "TwilioAuthToken",
                From = "+17259990509")] IAsyncCollector<CreateMessageOptions> textMessageCollector,
            /* The parameter populated based on HTTP Trigger placeholder value.
            *
            * The value of this parameter will be extracted from the HTTP Trigger Route value ('Send/{guid}')
            * and it will match the value of 'guid' placeholder's value.
            */
            string guid,
            ILogger log)
        {
            log.LogInformation($"Sending notifications for invoice: {guid}...");
            log.LogInformation($"Sending email notification for invoice: {guid}...");

            await mailCollector.AddAsync(
                await CreateMailAsync(invoice, invoiceStream));

            log.LogInformation($"Email for invoice: {guid} has been sent.");
            log.LogInformation($"Sending text message notification for invoice: {guid}...");

            await textMessageCollector.AddAsync(CreateTextMessage(invoice));

            log.LogInformation($"Text notification for invoice: {guid} has been sent.");

            return new JsonResult(invoice);
        }

        private async static Task<SendGridMessage> CreateMailAsync(
            Invoice invoice,
            Stream invoiceStream)
        {
            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress("test@example.com", "Sender"));
            message.SetSubject($"Invoice {invoice.ID}");
            message.AddTo(new EmailAddress(invoice.MailTo, "Receiver"));

            message.AddContent("text/html", $"<strong>Invoice {invoice.ID}.</strong>");
            await message.AddAttachmentAsync($"{invoice.ID}.pdf", invoiceStream);

            return message;
        }

        private static CreateMessageOptions CreateTextMessage(Invoice invoice) =>
            new CreateMessageOptions(
                new PhoneNumber(invoice.PhoneTo))
            {
                Body = $"Twoja faktura została wysłana ({invoice.ID})."
            };
    }
}
