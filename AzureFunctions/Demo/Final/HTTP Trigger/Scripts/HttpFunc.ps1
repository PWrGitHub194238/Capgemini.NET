$projectDir = '.\Projects\HttpFunc'

if (Test-Path -Path "${projectDir}") {
    Write-Host 'Re-creating HttpTrigger project'
    Remove-Item `
		-Path "${projectDir}" `
		-Recurse `
		-Force
} else {
    Write-Host 'Creating HttpTrigger project'
}

func init "${projectDir}" `
	--worker-runtime dotnet `
    > $null
	
Set-Location -Path ${projectDir}

New-Item `
	-Name 'Functions' `
	-Type Directory `
    > $null

Set-Location `
	-Path '.\Functions' `
    > $null
	
func new `
	--language C# `
	--template HttpTrigger `
	--name 'WeatherForecast' `
    > $null
	
Set-Location `
	-Path '..\..\..' `
    > $null
	
Copy-Item `
	-Path '.\Files\WeatherForecast.cs' `
	-Destination '.\Projects\HttpFunc\Functions\' `
    > $null
	
Set-Location `
	-Path ${projectDir} `
    > $null

Write-Host 'Running HttpTrigger project'
func start `
	--port 5002