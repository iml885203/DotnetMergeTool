# Uninstall MergeTool
if [ -f "/usr/local/bin/MergeTool" ]; then
    sudo rm /usr/local/bin/MergeTool
    echo "Removed MergeTool from /usr/local/bin"
else
    echo "MergeTool not found in /usr/local/bin"
fi

# Remove aliases from the shell configuration file
START_COMMENT="# MergeTool aliases - Begin"
END_COMMENT="# MergeTool aliases - End"

if [ -f "$HOME/.zshrc" ]; then
    CONFIG_FILE="$HOME/.zshrc"
elif [ -f "$HOME/.bashrc" ]; then
    CONFIG_FILE="$HOME/.bashrc"
else
    echo "Shell configuration file not found: ~/.zshrc or ~/.bashrc"
    exit 1
fi

if grep -q "$START_COMMENT" "$CONFIG_FILE"; then
    sed -i '' "/$START_COMMENT/,/$END_COMMENT/d" "$CONFIG_FILE"
    echo "Removed aliases from $CONFIG_FILE"
else
    echo "MergeTool aliases not found in $CONFIG_FILE"
fi

echo "Uninstalled successfully!"