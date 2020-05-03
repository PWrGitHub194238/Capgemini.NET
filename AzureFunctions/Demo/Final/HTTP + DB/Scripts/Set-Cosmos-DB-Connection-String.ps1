$localSettingsJsonPath = ".\HttpTrigger\local.settings.json"

az login

$subscriptionName = "Pay-As-You-Go Dev/Test"
$resourceGroupName = 'capgemini-azure-shotrs-func-02-http-db-trigger'

$functionAppCosmosDbName = "shortsfunchttpdbcosmosdb"

az account set `
	--subscription $subscriptionName

$resGroupExists = az group exists `
	--resource-group $resourceGroupName

if ($resGroupExists -eq $True)
{
    $cosmosDbKey = az cosmosdb keys list `
        --name $functionAppCosmosDbName `
        --resource-group $resourceGroupName `
        --query "primaryMasterKey"

    $cosmosDbKey = $cosmosDbKey -replace '"'

    $connectionString = "AccountEndpoint=https://shortsfunchttpdbcosmosdb.documents.azure.com:443/;AccountKey=${cosmosDbKey};"

    $localSettingsJson = @{
        IsEncrypted = $False;
        Values = @{
            AzureWebJobsStorage = "UseDevelopmentStorage=true";
            CosmosDbConnectionString = "${connectionString}";
            FUNCTIONS_WORKER_RUNTIME = "dotnet";
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