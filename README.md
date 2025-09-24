# Mobile 3D Game

A 3D mobile game built with Unity, optimized for mobile platforms (Android & iOS).

## Features

- **3D Graphics**: Modern 3D rendering with optimized shaders for mobile
- **Mobile Controls**: Touch-based controls with virtual joystick and buttons
- **Performance Optimized**: Efficient rendering and memory management for mobile devices
- **Cross-Platform**: Supports both Android and iOS platforms
- **Level System**: Multiple levels with increasing difficulty
- **Sound System**: Integrated audio with music and sound effects
- **UI System**: Responsive UI that adapts to different screen sizes

## Game Mechanics

- **Player Movement**: 3D character movement with jump mechanics
- **Collectibles**: Coins and power-ups scattered throughout levels
- **Obstacles**: Dynamic obstacles that challenge the player
- **Score System**: Point-based scoring with high score tracking
- **Level Progression**: Unlockable levels based on performance

## Technical Features

- **Mobile Optimization**: 
  - LOD (Level of Detail) system for 3D models
  - Texture compression for reduced memory usage
  - Efficient batching to reduce draw calls
  - Mobile-specific lighting and shadows

- **Controls**:
  - Virtual joystick for movement
  - Touch buttons for actions (jump, shoot, etc.)
  - Gesture recognition for special moves

- **Performance**:
  - Target 60 FPS on mid-range devices
  - Dynamic quality settings based on device performance
  - Memory pool management for objects

## Project Structure

```
Assets/
├── Scripts/           # C# game scripts
├── Models/            # 3D models and meshes
├── Materials/         # Shaders and materials
├── Textures/          # Texture assets
├── Prefabs/           # Reusable game objects
├── Scenes/            # Game scenes
├── Audio/             # Music and sound effects
└── UI/                # User interface elements

ProjectSettings/       # Unity project configuration
Packages/             # Package dependencies
```

## Getting Started

### Prerequisites

- Unity 2023.3.0f1 or later
- Android SDK (for Android builds)
- Xcode (for iOS builds on macOS)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/sarkarsubhadip604-dotcom/mobile-3d-game.git
   ```

2. Open the project in Unity Hub

3. Install required packages via Package Manager

4. Open the main scene: `Assets/Scenes/MainMenu.unity`

### Building for Mobile

#### Android
1. Switch platform to Android in Build Settings
2. Configure Android SDK path in Preferences
3. Set target API level (minimum API 21)
4. Build and deploy to device

#### iOS  
1. Switch platform to iOS in Build Settings
2. Configure iOS build settings
3. Build Xcode project
4. Open in Xcode and deploy to device

## Controls

- **Movement**: Virtual joystick (bottom-left)
- **Jump**: Jump button (bottom-right)
- **Camera**: Touch and drag to rotate camera
- **Menu**: Pause button (top-right)

## Performance Guidelines

- Target devices: Mid-range smartphones (2GB+ RAM)
- Resolution: Adaptive (720p-1080p based on device)
- FPS: 60 FPS target, 30 FPS minimum
- Battery optimization: Efficient rendering pipeline

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For questions or support, please open an issue in the GitHub repository.