# TaskbarHider

A lightweight Windows 10/11 application that automatically hides the taskbar after a configurable period of inactivity and smoothly reveals it when the cursor approaches the bottom edge of the screen.

## Features

✅ **Auto-hide Taskbar** - Hides after configurable delay (1-30 seconds of inactivity)  
✅ **Smart Detection** - Shows taskbar when cursor touches bottom edge  
✅ **System Tray Icon** - Right-click for settings and exit  
✅ **Start with Windows** - Optional auto-startup via registry  
✅ **Multi-Monitor Support** - Works seamlessly across multiple displays  
✅ **Game Detection** - Auto-disables during fullscreen games (Steam, Valorant, Minecraft, etc.)  
✅ **Low Resource Usage** - ~15-25 MB RAM, minimal CPU (~0.1%)  
✅ **Persistent Settings** - Saves configuration to `%AppData%\TaskbarHider\settings.json`  

## System Requirements

- Windows 10/11
- .NET 8 Runtime
- Administrator privileges (for startup registry access)

## Installation & Build

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (optional, or use CLI)

### Build from Source

```bash
# Clone the repository
git clone https://github.com/TheSlayer-1/TaskbarHider.git
cd TaskbarHider

# Restore dependencies
dotnet restore

# Build the project
dotnet build -c Release

# Run the application
dotnet run
```

### Compiled Executable

After building, the executable is located at:
```
.\bin\Release\net8.0-windows\TaskbarHider.exe
```

## Usage

1. **Launch the application** - TaskbarHider will start and minimize to the system tray
2. **Access Settings** - Right-click the tray icon → "Settings"
3. **Adjust Settings:**
   - **Hide Delay** - Set delay (1-30 seconds) before taskbar hides
   - **Start with Windows** - Enable to run automatically on startup
   - **Game Detection** - Auto-show during fullscreen games
   - **Multi-Monitor Support** - Enable for multi-display setups

4. **Show Taskbar** - Move your mouse cursor to the bottom edge of the screen

## Project Structure

```
TaskbarHider/
├── Models/
│   └── AppSettings.cs           # Settings model and persistence
├── Services/
│   ├── TaskbarService.cs        # Taskbar show/hide logic
│   ├── MouseActivityMonitor.cs  # Mouse position tracking
│   ├── GameDetectionService.cs  # Game process detection
│   └── SettingsManager.cs       # Settings management & registry
├── Views/
│   ├── SettingsWindow.xaml      # Settings UI
│   └── SettingsWindow.xaml.cs   # Settings logic
├── App.xaml                     # Application entry point
├── App.xaml.cs                  # Application startup logic
├── MainWindow.xaml              # Main window (hidden)
├── MainWindow.xaml.cs           # Main window logic with tray icon
├── TaskbarHider.csproj          # Project configuration
└── README.md                    # This file
```

## How It Works

1. **Mouse Monitoring** - `MouseActivityMonitor` checks cursor position every 100ms
2. **Inactivity Detection** - After the configured delay, taskbar hides if cursor isn't at screen bottom
3. **Bottom Edge Detection** - When cursor touches bottom edge (within taskbar height), taskbar shows
4. **Game Detection** - `GameDetectionService` monitors for known game processes and keeps taskbar visible
5. **Persistent Settings** - Settings saved to JSON in AppData folder

## Settings File

Settings are stored at: `%AppData%\TaskbarHider\settings.json`

Example:
```json
{
  "HideDelaySeconds": 3,
  "StartWithWindows": false,
  "EnableGameDetection": true,
  "EnableMultiMonitor": true
}
```

## Supported Games (Auto-Detection)

- Steam, Dota 2, CS:GO, Valorant
- Minecraft, Roblox
- GTA 5, GTA 6
- Overwatch 2, Apex Legends
- Fortnite, PUBG
- And more...

## Troubleshooting

### Taskbar won't hide
- Check that the delay is set correctly in Settings
- Ensure you're not moving the mouse
- Try disabling Game Detection if a game is running

### Application won't start
- Ensure .NET 8 Runtime is installed
- Run with administrator privileges
- Check `%AppData%\TaskbarHider\settings.json` for corruption

### Multi-monitor issues
- Ensure "Enable multi-monitor support" is checked in Settings
- Taskbar detection works on all connected displays

## Performance

- **RAM Usage:** 15-25 MB
- **CPU Usage:** ~0.1% (minimal background monitoring)
- **Timer Intervals:** Mouse check every 100ms, Inactivity check every 500ms, Game detection every 2s

## License

MIT License - Free to use and modify

## Contributing

Feel free to submit issues, fork the repository, and create pull requests for improvements.

## Future Enhancements

- [ ] Theme customization (dark/light mode)
- [ ] Custom hotkeys for manual toggle
- [ ] Animation effects for taskbar hide/show
- [ ] Additional game detection database
- [ ] Portable executable (no .NET runtime required)
- [ ] Setup installer (MSI/WiX)

---

**Made with ❤️ for Windows power users**