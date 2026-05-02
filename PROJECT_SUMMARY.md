# PROJECT SUMMARY & TECHNICAL ASSET OVERVIEW

This document outlines the architecture, game mechanics, global scripts, and reusable third-party assets integrated into the Universal-Game-Builder template for the PLAKOD Rehabilitation Game hackathon.

## 1. Core Architecture & Folder Structure

The structural layout of the Unity project is designed to separate global utilities from game-specific logic, offering teams a clean baseline to begin development.

- **`Assets/[Example]*` Folders:**
  - `[Example]BridgeBuilder/`
  - `[Example]Catching Game/`
  - `[Example]Flight Game/`
  - `[Example]Gravity Maze/`
  - _Purpose:_ These are functional micro-game templates demonstrating the "1 Game = 1 Task" philosophy. They show how to isolate single-input mechanics.
- **`Assets/[Your Games]/`:**
  - _Purpose:_ The designated sandbox directory meant for the hackathon team to build out their custom rehabilitation game without conflicting with existing example structures.
- **`Assets/Scripts Manager/`:**
  - _Purpose:_ Contains core singleton manager scripts orchestrating the macro-level systems of the game.
  - `GameInputManager.cs`: Maps hardware signals (binary outputs from sensors like the Limit Switch, LDR, Tilt Sensor) to logical actions (`MoveLeft`, `LiftPlatform`, etc.).
  - `GameManager.cs`: Handles overarching game state.
  - `SpineAnimationController.cs`: A generalized wrapper for invoking Spine skeletal animations.
  - `CameraShake.cs`: Basic procedural feedback utility for impact feedback.
- **`Assets/Plugins/`:**
  - Contains **DOTween**, the industry-standard procedural animation and tweening engine for Unity. Used heavily for smooth interpolation, UI animations, and physical tweens.

## 2. Input System Integration (Hardware -> Unity)

The bridge between the PLAKOD physical rehabilitation device and the Unity software is abstracted by the `GameInputManager.cs`.

- **Input Types:** Defined by `InputActionType` (e.g., `MoveLeft`, `LiftLeftPlatform`, `MoveUp`).
- **Mechanism:** The physical device relays discrete inputs (e.g., IR sensor triggering or Copper Tape closing a circuit) which are mapped as standard Keyboard keys. The `InputBinding` class allows dynamic configuration, checking multiple fallback keys per logical action `Input.GetKey(key)`.
- _AI Implementation Note:_ Developers should exclusively poll inputs via `GameInputManager.Instance.IsActionPressed(type)` rather than raw Unity Input checks to maintain decoupling.

## 3. Pre-Installed Assets & Packages Available for Use

The template provides professional-grade assets designed to jumpstart the visual and gameplay feedback loops, which are critical for patient desirability and motivation.

### A. Spine 2D (`Assets/Spine/` & `Assets/Spine Examples/`)

- A powerful 2D skeletal animation system runtime.
- _Features:_ Dynamic physics on bones, inverse kinematics (IK), mesh deformations, and blend trees. It allows characters to feel dynamic without consuming excessive performance. Included with extensive integration examples.

### B. Fantazia Animated 2D Monsters (`Assets/Fantazia Animated 2D Monsters/`)

- A comprehensive collection of professionally animated 2D monster characters.
- _Format:_ Both Sprite Sequences and Source Spine formats are included, allowing for dynamic modifications in Unity using the provided Spine plugins. Useful as targets, guides, or interactive avatars to build rehabilitation mechanics.

### C. Epic Toon FX (`Assets/Epic Toon FX/`)

- A cartoon/stylized VFX particle library.
- _Contents:_ Contains 300+ VFX including explosions, elemental spells, hit impacts, environment particle fields, and UI feedback effects.
- _Use Case:_ Visual rewards for patients. Instantiating a rewarding VFX upon completing an anatomically accurate physical motion increases dopamine and replayability constraints.

### D. DOTween (`Assets/Plugins/Demigiant/DOTween/`)

- _Contents:_ Tweening engine for scripted animations.
- _Use Case:_ Used for UI element sliding, bouncing game objects, moving platforms (like in the `Gravity Maze`), or interpolating numerical values over time.

### E. TextMesh Pro (`Assets/TextMesh Pro/`)

- _Contents:_ Advanced text rendering engine.
- _Use Case:_ Used for rendering high-fidelity, dynamic score counters, dialogs, and HUD indicators with support for custom shaders (outline, glow).

## 4. Recommendation for Success

To succeed within the 3 metrics (Feasibility, Desirability, Novelty), teams should focus on isolating the `GameInputManager` to read the PLAKOD sensors and apply immediate, rewarding visual feedback using _DOTween_ and _Epic Toon FX_. Rather than modifying the complex Spine skeleton implementations, use the existing `Fantazia` prefabs. Build the final delivery strictly inside `[Your Games]` to keep source control clear.
