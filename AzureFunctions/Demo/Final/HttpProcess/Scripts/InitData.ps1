$projectDir = '.\InitData'

Set-Location `
    -Path ${projectDir} `
    > $null

Write-Host 'Building ASP.NET Core console InitData project' `
    -ForegroundColor Green

dotnet build `
    > $null

Write-Host 'Running ASP.NET Core console InitData project' `
    -ForegroundColor Green
    
dotnet run