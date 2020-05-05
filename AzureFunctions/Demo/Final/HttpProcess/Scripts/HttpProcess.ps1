$projectDir = '.\HttpProcess'

Set-Location `
	-Path ${projectDir} `
    > $null

Write-Host 'Building Azure Functions project' `
    -ForegroundColor Green

dotnet build `
    > $null

Write-Host 'Running Azure Functions project' `
    -ForegroundColor Green

func start