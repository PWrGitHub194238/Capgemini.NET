$appSettingsJsonPath = ".\InitData\appsettings.json"
$localSettingsJsonPath = ".\HttpProcess\local.settings.json"

az login

$subscriptionName = "Pay-As-You-Go Dev/Test"
$resourceGroupName = 'capgemini-azure-shotrs-func-03-http-process'

$functionAppStorageName = "shortsfunchttpprocstore"
$functionAppCosmosDbName = "shortsfunchttpprocesscosmosdb"

az account set `
	--subscription $subscriptionName

$resGroupExists = az group exists `
	--resource-group $resourceGroupName

if ($resGroupExists -eq $True)
{
    $storageAccountKey = (az storage account keys list `
        --account-name $functionAppStorageName `
        --resource-group $resourceGroupName `
        --query "[?keyName == 'key1'].value")[1]

    $storageAccountKey = $storageAccountKey `
        -replace '"' `
        -replace ' ' 

    $storageAccountConnectionString = "DefaultEndpointsProtocol=https;AccountName=shortsfunchttpprocstore;AccountKey=${storageAccountKey};EndpointSuffix=core.windows.net"

    $cosmosDbKey = az cosmosdb keys list `
        --name $functionAppCosmosDbName `
        --resource-group $resourceGroupName `
        --query "primaryMasterKey"

    $cosmosDbKey = $cosmosDbKey `
        -replace '"'

    $cosmosDbConnectionString = "AccountEndpoint=https://shortsfunchttpprocesscosmosdb.documents.azure.com:443/;AccountKey=${cosmosDbKey};"


    $appSettingsJson = @{
        StorageAccount = @{
            BlobContainerName = "invoices";
        }
        ConnectionStrings = @{
            StorageAccount = $storageAccountConnectionString;
            CosmosDB = $cosmosDbConnectionString;
        }
    } | ConvertTo-Json

    if (Test-Path -Path $appSettingsJsonPath) {
        Remove-Item `
            -Path $appSettingsJsonPath `
            > $Null
    }

    New-Item `
        -Path $appSettingsJsonPath `
        -ItemType File `
        -Value $appSettingsJson `
        > $Null


    $localSettingsJson = @{
        IsEncrypted = $False;
        Values = @{
            FUNCTIONS_WORKER_RUNTIME = "dotnet"
            AzureWebJobsStorage = $storageAccountConnectionString;
            CosmosDBConnectionString = $cosmosDbConnectionString;
            SendGridApiKey = "SEND_GRID_API_KEY";
            TwilioAccountSid =  "TWILIO_ACCOUNT_SID";
            TwilioAuthToken = "TWILIO_AUTH_TOKEN";
        }
    } | ConvertTo-Json

    if (Test-Path -Path $localSettingsJsonPath) {
        Remove-Item `
            -Path $localSettingsJsonPath `
            > $Null
    }

    New-Item `
        -Path $localSettingsJsonPath `
        -ItemType File `
        -Value $localSettingsJson `
        > $Null
}