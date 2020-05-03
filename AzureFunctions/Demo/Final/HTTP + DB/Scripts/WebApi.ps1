$projectDir = '.\WebApi'

Set-Location `
    -Path ${projectDir} `
    > $null

Write-Host 'Building ASP.NET Core Web API project' `
    -ForegroundColor Green

dotnet build `
    > $null

Write-Host 'Running ASP.NET Core Web API project' `
    -ForegroundColor Green
    
dotnet run