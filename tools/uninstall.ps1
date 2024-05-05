# Remove directory
Remove-Item -Path 'C:\Program Files\MergeTool' -Recurse -Force

# Remove from Env
$existingPath = [System.Environment]::GetEnvironmentVariable('Path', [System.EnvironmentVariableTarget]::Machine)
$newPath = $existingPath -replace ';C:\\Program Files\\MergeTool', ''
[System.Environment]::SetEnvironmentVariable('Path', $newPath, [System.EnvironmentVariableTarget]::Machine)

# Clean up aliases from profile
$profilePath = $PROFILE

if (Test-Path $profilePath) {
    $content = Get-Content -Path $profilePath -Raw
    $pattern = "(?s)# MergeTool Alias - Begin.*?# MergeTool Alias - End"
    $newContent = $content -replace $pattern
    Set-Content -Path $profilePath -Value $newContent
    Write-Host "Aliases have been removed from the PowerShell configuration file: $profilePath"
} else {
    Write-Host "PowerShell configuration file not found: $profilePath"
}

Write-Host "MergeTool has been uninstalled successfully!"
