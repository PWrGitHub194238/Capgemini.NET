$filesDir = '.\Files'
$projectDir = '.\Projects\WebApplication1'
$launchSettingsDir = "${projectDir}\Properties"
$launchSettingsPath = "${launchSettingsDir}\launchSettings.json"
$launchSettingsBakPath = "${launchSettingsDir}\launchSettings.json.bak"

Move-Item `
    -Path "${launchSettingsPath}" `
    -Destination "${launchSettingsBakPath}" `
    -Force
Copy-Item `
    -Path "${filesDir}\01.json" `
    -Destination "${launchSettingsDir}\launchSettings.json" `
    -Force