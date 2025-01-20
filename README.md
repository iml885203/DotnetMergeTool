# Merge Tool

A powerful Git branch merging tool that makes your merge workflow smoother and safer.

![](./readme/demo.gif)

![](./readme/gmip-demo.gif)

## Features 

- Smart branch merging
- Enhanced error handling
- Auto-rollback to original branch on errors
- Quick push option support
- Cross-platform support (Linux, MacOS, Windows)
- Amazon Q autocomplete (MacOS only)

## Table of Contents

<!-- toc -->

- [Usage](#usage)
- [Installation](#installation)
  * [Linux](#linux)
  * [MacOS](#macos)
  * [Windows](#windows)
- [Uninstall](#uninstall)
  * [Linux](#linux-1)
  * [MacOS](#macos-1)
  * [Windows](#windows-1)
- [Why need Merge Tool?](#why-need-merge-tool)
  * [See More...](#see-more)
- [CLI completions (macOS only)](#cli-completions-macos-only)
  * [How to setup Amazon Q Autocomplete for MergeTool](#how-to-setup-amazon-q-autocomplete-for-mergetool)
- [Contributing](#contributing)
- [License](#license)

<!-- tocstop -->

## Usage
```bash
MergeTool {branch}           # Merge specified branch
MergeTool {branch} --push    # Merge and push to remote
```

Aliases:
```bash
gmi {branch}    # Same as MergeTool {branch}
gmip {branch}   # Same as MergeTool {branch} --push
```

## Installation

### Linux
```bash
bash -c "$(curl -fsSL https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/install-linux.sh)"
```

### MacOS
```bash
bash -c "$(curl -fsSL https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/install-osx.sh)"
```

### Windows

Open PowerShell as Administrator and run the following command:

```powershell
powershell -command "& {Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/install.ps1'))}"
```

## Uninstall

### Linux
```bash
bash -c "$(curl -fsSL https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/uninstall.sh)"
```

### MacOS
```bash
bash -c "$(curl -fsSL https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/uninstall.sh)"
```

### Windows
```powershell
powershell -command "& {Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/uninstall.ps1'))}"
```

## Why need Merge Tool?

Merge Tool offers the following advantages:

- More user-friendly interface
- Robust error handling mechanism
- Auto-rollback feature to prevent stuck states after merge failures
- Improved development efficiency and code quality

### [See More...](./why-need-merge-tool.md)

## CLI completions (macOS only)

![](./q-autocomplete/readme/merge-tool-autocomplete.png)

### [How to setup Amazon Q Autocomplete for MergeTool](./q-autocomplete/README.md)

## Contributing

We welcome Pull Requests to improve this tool! Here's how to contribute:

1. Fork this project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details