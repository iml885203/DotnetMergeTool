dotnet build MergeTool/MergeTool.csproj
dotnet publish MergeTool/MergeTool.csproj -r osx-x64 --self-contained -o "publish"
sudo mv publish/MergeTool /usr/local/bin
rm -r publish