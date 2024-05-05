# 下載最新版本
Invoke-WebRequest -Uri 'https://github.com/iml885203/DotnetMergeTool/releases/latest/download/MergeTool-win-x64.zip' -OutFile 'MergeTool-win-x64.zip'

# 解壓縮文件到指定路徑
Expand-Archive -Path 'MergeTool-win-x64.zip' -DestinationPath 'C:\Program Files\MergeTool' -Force

# 設置環境變量
$existingPath = [System.Environment]::GetEnvironmentVariable('Path', [System.EnvironmentVariableTarget]::Machine)
if ($existingPath -split ';' -notcontains 'C:\Program Files\MergeTool') {
    $newPath = $existingPath + ";C:\Program Files\MergeTool"
    [System.Environment]::SetEnvironmentVariable('Path', $newPath, [System.EnvironmentVariableTarget]::Machine)
}

$env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine')

# 刪除下載的 zip 文件
Remove-Item -Path 'MergeTool-win-x64.zip'

Write-Host "Install MergeTool successfully! Please restart your command prompt or PowerShell to use MergeTool."
