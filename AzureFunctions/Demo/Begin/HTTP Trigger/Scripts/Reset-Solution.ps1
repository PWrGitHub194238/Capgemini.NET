$projectDir = '.\Projects'

if (Test-Path -Path "${projectDir}") {
    Write-Host 'Re-creating blank solution project for the demo...'
    Remove-Item `
		-Path "${projectDir}" `
		-Recurse `
		-Force
}

dotnet new sln `
    --output "${projectDir}" `
    > $null