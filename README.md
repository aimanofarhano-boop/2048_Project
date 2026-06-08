# 2048_Project

## Overview
A Unity implementation of the popular 2048 sliding tile puzzle game. This is a learning project designed to practice game development fundamentals in Unity.

## Project Description
2048_Project recreates the classic 2048 game mechanic where players combine numbered tiles to reach the target tile value of 2048. The game is built with Unity and features a complete game management system, scoring, and UI elements.

## Main Features

### Core Gameplay
- **Tile-based puzzle system** - Navigate and merge tiles on a game board
- **Tile Board Management** - Handles tile creation, movement, and combination logic
- **Game Over Detection** - Automatically detects when no more moves are possible

### Game Systems
- **Score Tracking** - Real-time score display during gameplay
- **High Score Persistence** - Saves and displays the player's highest score
- **Gold/Currency System** - Players start with 20 gold per new game
- **Game State Management** - Controls transitions between gameplay and game over states

### User Interface
- **Score Display** - Current game score shown via TextMesh Pro UI
- **High Score Display** - Best score saved from previous games
- **Gold Display** - Currency/resources indicator
- **Game Over Screen** - Modal UI displayed when the game ends with fade animations

## Project Structure
- **Assets/Scripts/** - Core game logic and managers
- **Assets/Prefabs/** - Reusable game objects (tiles, player, enemies, etc.)
- **Assets/Scenes/** - Game scenes
- **Assets/Sprites/** - Visual assets and graphics
- **Assets/Tiles/** - Tile-specific assets
- **Assets/Fonts/** - UI fonts (TextMesh Pro)
- **Assets/Settings/** - Game configuration files

## Technical Details
- **Engine:** Unity
- **UI Framework:** TextMesh Pro
- **Input System:** Unity Input System
- **Rendering:** Universal Render Pipeline (URP)

## Getting Started
1. Open the project in Unity
2. Load the main game scene
3. Run the scene to start playing
4. Combine tiles to reach 2048!
