# Merge Tool

Git merge tool

![](./readme/demo.gif)

![](./readme/gmip-demo.gif)

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
- [CLI completions for MergeTool(macOS only)](#cli-completions-for-mergetoolmacos-only)
  * [How to setup Amazon Q Autocomplete for MergeTool](#how-to-setup-amazon-q-autocomplete-for-mergetool)

<!-- tocstop -->

## Usage
```bash
MergeTool {branch}
MergeTool {branch} --push
```

Or you can use the alias:
```bash
gmi {branch}
gmip {branch}
```

## Installation

### Linux
```bash
bash -c "$(curl -fsSL https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/install-linux.sh)"
```

### MacOS
```bash
bash -c "$(curl -fsSL https://raw.githubusercontent.com/iml885203/DotnetMergeTool/main/tools/install-oxs.sh)"
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

## CLI completions for MergeTool(macOS only)

![](./q-autocomplete/readme/merge-tool-autocomplete.png)

### [How to setup Amazon Q Autocomplete for MergeTool](./q-autocomplete/README.md)