az login

$location = 'northeurope'
$subscriptionName = "Pay-As-You-Go Dev/Test"
$resourceGroupName = 'capgemini-azure-shotrs-func-02-http-db-trigger'

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

$functionAppName = "capgemini-azure-shorts-func-http-db"
$functionAppStorageName = "shortsfunchttpdbstore"
$functionAppCosmosDbName = "shortsfunchttpdbcosmosdb"
$functionAppCosmosDbDatabaseName = "weather-forecast-db"
$functionAppCosmosDbCollectionName = "weather-forecasts"
$functionAppCosmosDbPartictionKey = "/id"

az storage account create `
	--name $functionAppStorageName `
	--resource-group $resourceGroupName `
	--access-tier Cool `
	--kind StorageV2 `
	--location $location `
	--sku Standard_LRS

az functionapp create `
	--name $functionAppName `
	--resource-group $resourceGroupName `
	--storage-account $functionAppStorageName `
	--consumption-plan-location $location `
	--functions-version 3 `
	--os-type 'Windows' `
	--runtime 'dotnet'

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