# Download MergeTool
Invoke-WebRequest -Uri 'https://github.com/iml885203/DotnetMergeTool/releases/latest/download/MergeTool-win-x64.zip' -OutFile 'MergeTool-win-x64.zip'

# Create directory
Expand-Archive -Path 'MergeTool-win-x64.zip' -DestinationPath 'C:\Program Files\MergeTool' -Force

# Update Env
$existingPath = [System.Environment]::GetEnvironmentVariable('Path', [System.EnvironmentVariableTarget]::Machine)
if ($existingPath -split ';' -notcontains 'C:\Program Files\MergeTool') {
    $newPath = $existingPath + ";C:\Program Files\MergeTool"
    [System.Environment]::SetEnvironmentVariable('Path', $newPath, [System.EnvironmentVariableTarget]::Machine)
}

$env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine')

# Clean up
Remove-Item -Path 'MergeTool-win-x64.zip'

# Set aliases
$aliasesScript = @"
# MergeTool Alias - Begin
function gmi {
    MergeTool `$args
}

function gmip {
    MergeTool --push `$args
}
# MergeTool Alias - End
"@

$profilePath = $PROFILE

if (-not (Test-Path $profilePath)) {
    New-Item -Path $profilePath -ItemType File -Force | Out-Null
}

if (-not (Get-Content $profilePath | Select-String -Pattern "# MergeTool Alias - Begin")) {
    Add-Content -Path $profilePath -Value $aliasesScript
    Write-Host "Aliases script has been added to the PowerShell configuration file: $profilePath"
} else {
    Write-Host "Aliases script already exists in the PowerShell configuration file: $profilePath"
}

Write-Host "Install MergeTool successfully! Please restart your PowerShell to use MergeTool."
