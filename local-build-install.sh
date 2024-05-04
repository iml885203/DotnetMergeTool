dotnet build MergeTool/MergeTool.csproj --configuration Release --no-restore
dotnet publish MergeTool/MergeTool.csproj -c Release -r osx-x64 --self-contained -o "publish"
sudo mv publish/MergeTool /usr/local/bin
rm -r publish