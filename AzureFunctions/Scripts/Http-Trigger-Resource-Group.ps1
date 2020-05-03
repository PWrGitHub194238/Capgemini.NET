az login

$location = 'northeurope'
$subscriptionName = "Pay-As-You-Go Dev/Test"
$resourceGroupName = 'capgemini-azure-shotrs-func-01-http-trigger'

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

$functionAppName = "capgemini-azure-shorts-func-http"
$functionAppStorageName = "shortsfunchttpstore"

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
	--os-type 'Windows' `
	--runtime 'dotnet' `
	--runtime-version 2