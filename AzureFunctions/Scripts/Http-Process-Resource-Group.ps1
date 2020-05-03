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

$functionAppBlobContainerName = "invoices"

az storage container create `
	--name $functionAppBlobContainerName `
	--account-name $functionAppStorageName `
	--public-access off

$functionAppName = "capgemini-azure-shorts-func-http-process"

az functionapp create `
	--name $functionAppName `
	--resource-group $resourceGroupName `
	--storage-account $functionAppStorageName `
	--consumption-plan-location $location `
	--os-type 'Windows' `
	--runtime 'dotnet' `
	--runtime-version 2

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