$projectDir = '.\Projects\Http'

if (Test-Path -Path "${projectDir}") {
    Write-Host 'Re-creating HttpTrigger project'
    Remove-Item `
		-Path "${projectDir}" `
		-Recurse
} else {
    Write-Host 'Creating HttpTrigger project'
}

dotnet new func `
    --output "${projectDir}" `
    > $null
	
dotnet new http `
    --namespace 'Http.Functions' `
	--AccessRights 'Anonymous' `
    --output "${projectDir}\Functions" `
    > $null
	
Copy-Item `
	-Path '.\Files\WeatherForecastFunc.cs' `
	-Destination '.\Projects\Http\Functions\'

Set-Location -Path ${projectDir}

Write-Host 'Running HttpTrigger project'
func start --port 5000