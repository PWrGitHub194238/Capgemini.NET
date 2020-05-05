$projectDir = '.\Samples\WebApi'

if (Test-Path -Path "${projectDir}") {
    Write-Host 'Re-creating ASP.NET Core Web API project'
    Remove-Item `
		-Path "${projectDir}" `
		-Recurse `
		-Force
} else {
    Write-Host 'Creating ASP.NET Core Web API project'
}

dotnet new webapi `
    --auth 'None' `
    --no-https `
    --framework 'netcoreapp3.1' `
    --output "${projectDir}" `
    > $null

Set-Location -Path ${projectDir}

Write-Host 'Running ASP.NET Core Web API project'
Write-Host '[GET] http://localhost:5000/WeatherForecast' `
	-ForegroundColor Green
dotnet run `
	--server.urls http://localhost:5000