az login

$resourceGroupName = az group list --query [0].name
$location = 'northeurope'
$functionAppName = 'capfuncapp'
$functionAppStorageName = "${functionAppName}storeacc"

# az storage account create `
# 	--name $functionAppStorageName `
# 	--resource-group $resourceGroupName `
# 	--access-tier Cool `
# 	--kind StorageV2 `
# 	--location $location `
# 	--sku Standard_LRS

# az functionapp create `
# 	--name $functionAppName `
# 	--resource-group $resourceGroupName `
# 	--storage-account $functionAppStorageName `
# 	--consumption-plan-location $location `
# 	--disable-app-insights `
# 	--os-type 'Windows' `
# 	--runtime 'dotnet' `
# 	--runtime-version 2


$functionAppName = 'capfuncappvs'
$functionAppStorageName = "${functionAppName}storeacc"
az storage account create `
	--name $functionAppStorageName `
	--resource-group $resourceGroupName `
	--access-tier Cool `
	--kind StorageV2 `
	--location $location `
	--sku Standard_LRS