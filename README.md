# Kitchen Chaos - An Unity 3D Cooking Game

A fast paced cooking game built with Unity 3D where the player has to mange and prepare orders and deliver dishes before the timer runs out.

![Gameplay Screenshot](https://github.com/VinitVerma06/KitchenChaos/blob/f9a246b90ab2bb9de6ea9bb6f4fd8028d8a0a513/Screenshots/Kitchen%20Chaos_GamplayImg.png)

Platforms Supported:
- 🖥️ Windows PC (controller support)
- 📱 Android

## ✨ Key Features

### Core Gameplay
- **Recipe System**: Multiple recipes with specific indgredients.
- **Order System**: Random customer orders with visual indication upon completion.
- **Time Management**: Cook and deliver as many orders before the timer runs out.

### Technical Features
- **Cross-Platform Input**: Support for keyboard/mouse (PC) and touch controls (Android).
- **Tutorial System**: Event-driven tutorial guiding players through core gameplay mechanics.
- **Level Progression System**: Sequential level unlocking with save data persistence.
- **State Management**: Robust game state system (WaitingToStart, Playing, GameOver).
- **Mobile UI**: Custom on-screen joystick and touch buttons with variable speed control.

### Polish & UX
- **Cinemachine Camera**: Smooth camera transitions between main menu and level selection.
- **Audio System**: Separate music and sound effects with volume controls.
- **Key Rebinding**: Fully customizable keyboard controls (PC and controller only).
- **Platform Detection**: Automatic UI adaptation for PC vs Mobile.
- **Progress Saving**: PlayerPrefs-based save system for level completion tracking.

## 🛠️ Technologies & Tools

**Engine & Framework:**
- Unity 2022.3.13f1 LTS 
- C# (.NET Standard 2.1)

**Key Unity Packages:**
- Unity Input System (New Input System)
- Cinemachine (Camera management)
- TextMeshPro (UI text rendering)
- System (Events and Actions)

**Architecture Patterns:**
- Event-driven architecture
- Singleton pattern (for managers)
- State machine pattern (game states, tutorials)
- Static utility classes (level management, loader)

**Version Control:**
- Git
- GitHub

## ⚙️ Game Mechanics

### Kitchen Counters
- **Container Counter**: Source of ingredients (tomato, cabbage, cheese, etc.)
- **Cutting Counter**: Used to slice ingredients with progress bar.
- **Stove Counter**: Cook ingredients with burning mechanic.
- **Plate Counter**: Spawns plates.
- **Delivery Counter**: Place completed recipes.
- **Trash Counter**: Dispose of unwanted items (burnt ingredient).

### Recipes
Players must prepare dishes by combining specific ingredients:
- 🍅 Simple dishes (e.g., tomato -> sliced tomato).
- 🍔 Complex recipes (e.g., Cheese Burger: Bread + Cooked meat patty + Cheese slices + cabbage slices).

### Progression
- Currently only **3 Levels** are implemented.
- Level unlocking: Complete previous level to unlock next one.
- **Placeholder system**: 6 button slots for future expansion.
- Progress saved locally using PlayerPrefs.

## 📱 Controls

### PC (Keyboard & Mouse)
- **WASD**: Movement
- **E**: Interact (pickup / place)
- **F**: Alternative Interact (start day, end day, cut)
- **ESC**: Pause menu
- **Fully rebindable** via Options menu

### Android (Touch)
- **Virtual Joystick**: Movement with variable speed
- **Touch Buttons**: Interact and alternative interact
- On-screen controls automatically shown on mobile devices

## 🚀 Future Enhancements

If I were to continue development, potential additions would include:

- [ ] Additional levels (4-10+)
- [ ] Day wise progression
- [ ] Money earning system
- [ ] More recipes, ingredients and cooking stations
- [ ] Power-ups and upgradable cooking utensils
- [ ] More visual polish (particle effects, animations)

## 🙏 Acknowledgments

- **Tutorial Reference**: [CodeMonkey's Unity Beginner/Intermediate tutorial](https://youtu.be/AmGSEH7QcDg?si=kROL3MWwa_InJVpr) 
- **Assets**: [Desk Bell by Jarlan Perez via Poly Pizza](https://poly.pizza/m/1BXI7wJyaG8) [CC-BY](https://creativecommons.org/licenses/by/3.0/)
- **Sound Effects**: [Bell SFX by freesound_community from Pixabay](https://pixabay.com/sound-effects/film-special-effects-service-bell-ring-14610/)

## 📧 Contact

**Vinit Verma**
- GitHub: [@VinitVerma06](https://github.com/VinitVerma06)
- LinkedIn: [Vinit Verma](https://www.linkedin.com/in/vinit-verma-5908b8257/)
- Email: vinit07verma06@gmail.com
