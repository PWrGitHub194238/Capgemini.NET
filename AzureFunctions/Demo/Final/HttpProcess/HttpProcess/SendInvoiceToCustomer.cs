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
        [FunctionName("SendInvoiceToCustomer")]
        [return: Queue("invoices-queue", Connection = "AzureWebJobsStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function, "POST", Route = "Send/{guid}")] HttpRequest req,
            [CosmosDB(
                databaseName: "invoice-db",
                collectionName: "invoices",
                ConnectionStringSetting = "CosmosDBConnectionString",
                Id = "{guid}",
                PartitionKey = "{guid}")] Invoice invoice,
            [Blob(
                "invoices/{guid}",
                FileAccess.Read,
                Connection = "AzureWebJobsStorage")] Stream invoiceStream,
            [SendGrid(
                ApiKey = "SendGridApiKey")] IAsyncCollector<SendGridMessage> mailCollector,
            [TwilioSms(
                AccountSidSetting = "TwilioAccountSid",
                AuthTokenSetting = "TwilioAuthToken",
                From = "+12057514461")] IAsyncCollector<CreateMessageOptions> textMessageCollector,
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
            var message = new SendGridMessage();
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
                Body = $"Twoja faktura zosta³a wys³ana ({invoice.ID})."
            };
    }
}
