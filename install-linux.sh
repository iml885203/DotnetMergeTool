curl -L -o MergeTool-linux-x64.tar.gz https://github.com/iml885203/DotnetMergeTool/releases/latest/download/MergeTool-linux-x64.tar.gz
tar -xzf MergeTool-linux-x64.tar.gz
rm MergeTool-linux-x64.tar.gz
sudo mv MergeTool-linux-x64/MergeTool /usr/local/bin
rm -r MergeTool-linux-x64

MergeTool --version
echo "installed successfully!"
