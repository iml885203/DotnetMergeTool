# Download the latest version
curl -L -o MergeTool-osx-x64.tar.gz https://github.com/iml885203/DotnetMergeTool/releases/latest/download/MergeTool-osx-x64.tar.gz
tar -xzf MergeTool-osx-x64.tar.gz
rm MergeTool-osx-x64.tar.gz

# Make the binary executable
sudo mv MergeTool-osx-x64/MergeTool /usr/local/bin
rm -r MergeTool-osx-x64
echo "Moved MergeTool to /usr/local/bin"

# Add aliases to the shell configuration file
if [ -f "$HOME/.zshrc" ]; then
    CONFIG_FILE="$HOME/.zshrc"
elif [ -f "$HOME/.bashrc" ]; then
    CONFIG_FILE="$HOME/.bashrc"
else
    echo "Shell configuration file not found: ~/.zshrc or ~/.bashrc"
    exit 1
fi

ALIASES=(
    ""
    "# MergeTool aliases - Begin"
    "alias gmi=\"MergeTool gmi\""
    "alias gmip=\"MergeTool gmip\""
    "# MergeTool aliases - End"
)

if grep -q "# MergeTool aliases - Begin" "$CONFIG_FILE"; then
    echo "MergeTool aliases already exist in $CONFIG_FILE"
else
    # 添加别名到配置文件中
    for ALIAS in "${ALIASES[@]}"; do
        echo "$ALIAS" >> "$CONFIG_FILE"
    done
    echo "Added alias to $CONFIG_FILE"
fi

# Show MergeTool Version
MergeTool --version

echo "Installed successfully! Please restart your shell to use MergeTool"

