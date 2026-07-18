# TaskbarHider

A lightweight Windows 10/11 application that automatically hides the taskbar after a period of inactivity and shows it when you move your mouse to the bottom of the screen.

## Features

✅ **Auto-hide taskbar** - After configurable delay (1-30 seconds)  
✅ **Smart detection** - Shows taskbar when cursor touches bottom edge  
✅ **System tray icon** - Right-click for settings  
✅ **Startup option** - Run automatically with Windows  
✅ **Multi-monitor support** - Works across all connected displays  
✅ **Game detection** - Auto-disables during full-screen games  
✅ **Lightweight** - Uses ~15-25 MB RAM, minimal CPU  
✅ **Persistent settings** - Saves preferences automatically  

## Installation

1. Download `TaskbarHider.exe` from the Releases page
2. Run the executable
3. The app will appear in your system tray (bottom-right corner)
4. Right-click the tray icon to access Settings

## Usage

- **Hide taskbar**: Move mouse away from bottom edge and wait for the configured delay
- **Show taskbar**: Move mouse to the bottom edge of the screen
- **Settings**: Right-click system tray icon → Settings
- **Exit**: Right-click system tray icon → Exit

## Settings

- **Hide Delay**: 1-30 seconds of inactivity before hiding
- **Start with Windows**: Automatically launch on boot
- **Game Detection**: Auto-show during fullscreen games
- **Multi-monitor Support**: Works with multiple displays

## Requirements

- Windows 10 or later
- .NET 8 Runtime (included in release)

## Building from Source

```bash
git clone https://github.com/TheSlayer-1/TaskbarHider.git
cd TaskbarHider
dotnet restore
dotnet build -c Release
dotnet run
```

## License

MIT License - Feel free to use and modify!