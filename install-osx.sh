curl -L -o MergeTool-osx-x64.tar.gz https://github.com/iml885203/DotnetMergeTool/releases/latest/download/MergeTool-osx-x64.tar.gz
tar -xzf MergeTool-osx-x64.tar.gz
rm MergeTool-osx-x64.tar.gz
sudo mv MergeTool-osx-x64/MergeTool /usr/local/bin
rm -r MergeTool-osx-x64

MergeTool --version
echo "installed successfully!"
