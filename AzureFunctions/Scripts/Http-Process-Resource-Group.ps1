az login

$location = 'northeurope'
$subscriptionName = "Pay-As-You-Go Dev/Test"
$resourceGroupName = 'capgemini-azure-shotrs-func-03-http-process'

az account set `
	--subscription $subscriptionName

$resGroupExists = az group exists `
	--resource-group $resourceGroupName

if ($resGroupExists -eq $True)
{
	az group delete `
		--resource-group $resourceGroupName `
		--yes
}

az group create `
	--location $location `
	--resource-group $resourceGroupName `

$functionAppStorageName = "shortsfunchttpprocstore"

az storage account create `
	--name $functionAppStorageName `
	--resource-group $resourceGroupName `
	--access-tier Cool `
	--kind StorageV2 `
	--location $location `
	--sku Standard_LRS

$accountKeys = az storage account keys list `
	--resource-group $resourceGroupName `
	--account-name $functionAppStorageName `
	--query [0].value

$functionAppBlobContainerName = "invoices"

az storage container create `
	--name $functionAppBlobContainerName `
	--account-name $functionAppStorageName `
	--public-access off `
    --account-key $accountKeys `
    --auth-mode key

$functionAppQueueName = "invoices-queue"

az storage queue create `
	--name $functionAppQueueName `
	--account-name $functionAppStorageName `
    --account-key $accountKeys `
    --auth-mode key

$functionAppName = "capgemini-azure-shorts-func-http-process"

az functionapp create `
	--name $functionAppName `
	--resource-group $resourceGroupName `
	--storage-account $functionAppStorageName `
	--consumption-plan-location $location `
	--functions-version 3 `
	--os-type 'Windows' `
	--runtime 'dotnet'
	
$functionAppCosmosDbName = "shortsfunchttpprocesscosmosdb"
$functionAppCosmosDbDatabaseName = "invoice-db"
$functionAppCosmosDbCollectionName = "invoices"
$functionAppCosmosDbPartictionKey = "/id"

az cosmosdb create `
	--name $functionAppCosmosDbName `
	--resource-group $resourceGroupName `
	--locations regionName=eastus failoverPriority=0 isZoneRedundant=False 

az cosmosdb sql database create `
	--account-name $functionAppCosmosDbName `
	--name $functionAppCosmosDbDatabaseName `
	--resource-group $resourceGroupName

az cosmosdb sql container create `
	--account-name $functionAppCosmosDbName `
	--database-name $functionAppCosmosDbDatabaseName `
	--name $functionAppCosmosDbCollectionName `
	--partition-key-path $functionAppCosmosDbPartictionKey `
	--resource-group $resourceGroupName