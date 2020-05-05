$projectDir = '.\Samples\Http'

if (Test-Path -Path "${projectDir}") {
    Write-Host 'Re-creating HttpTrigger project'
    Remove-Item `
		-Path "${projectDir}" `
		-Recurse `
		-Force
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
	-Path '.\Files\WeatherForecast.cs' `
	-Destination "${projectDir}\Functions\" `
    > $null

Set-Location `
	-Path ${projectDir} `
    > $null

Write-Host 'Running HttpTrigger project'
func start --port 5001