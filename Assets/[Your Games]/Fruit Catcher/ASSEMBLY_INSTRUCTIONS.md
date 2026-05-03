# Fruit Catcher - Unity Assembly Instructions

This guide provides step-by-step instructions to assemble the "Fruit Catcher" game inside the Unity Editor, using the provided scripts and maintaining the "button-press-to-catch" gameplay mechanic.

## Phase 1: Scene & Manager Setup

1. **Create the Scene**:
   - Navigate to `Assets/[Your Games]/Fruit Catcher/`.
   - Right-click > **Create > Scene**. Name it `FruitCatcherScene` and open it.
2. **Setup the Background**:
   - Right-click in the Hierarchy > **2D Object > Sprite**. Name it `Background`.
   - In the Sprite Renderer, assign a background image from your assets.
   - Set its **Order in Layer** to `-10` so it renders firmly behind all other game objects.
   - Scale it up so it completely covers the camera view.
3. **Setup the Managers Object**:
   - Right-click in the Hierarchy > **Create Empty**. Name it `GameManagers`.
   - **FruitGameManager**: Drag and drop the `FruitGameManager` script onto this object.
   - **GameInputManager**: Drag and drop the existing `GameInputManager` script onto this object (this bridges the PLAKOD hardware to the game). Expand `Bindings` and ensure `LiftLeftPlatform` (or another desired action) is bound to a key like the Spacebar.
4. **Setup the Camera**:
   - Select the `Main Camera` in the Hierarchy.
   - Click **Add Component** and search for `CameraShake`. Attach it.

## Phase 2: User Interface (UI) Setup

1. **Create the Canvas**:
   - Right-click the Hierarchy > **UI > Canvas**.
2. **MainMenu Panel**:
   - Right-click Canvas > **UI > Panel**. Name it `MainMenuPanel`. Set its color to a solid or semi-transparent color.
   - Add a child **Button (TextMeshPro)** and name it `StartButton`. Set its text to "Start Game".
3. **HUD (Heads Up Display)**:
   - Right-click Canvas > **UI > Text - TextMeshPro**. Name it `ScoreText`. Anchor it Top-Left. Set text to "Score: 0".
   - Right-click Canvas > **UI > Image**. Name it `HealthBarBackground`. Anchor Top-Right.
   - Add a child **Image** to `HealthBarBackground` named `HealthFill`. Change its Image Type to `Filled`, Fill Method to `Horizontal`. Set its color to Red.
4. **GameOver Panel**:
   - Right-click Canvas > **UI > Panel**. Name it `GameOverPanel`. Disable this panel by unchecking the box next to its name in the Inspector.
   - Add a TextMeshPro child that says "Game Over".
   - Add two child **Button (TextMeshPro)** objects: `RetryButton` and `QuitButton`.
5. **Link UI to Manager**:
   - Select `GameManagers`.
   - Drag your new UI elements from the Hierarchy into the corresponding slots on the `FruitGameManager` component in the Inspector (`Main Menu Panel`, `Start Button`, `Score Text`, `Health Fill`, `Game Over Panel`, `Retry Button`, `Quit Button`).

## Phase 3: The Player Hand (Action Catcher)

1. **Create the Hand**:
   - Hierarchy > **Create Empty**. Name it `PlayerHand`.
   - Set its transform position to `(X: 0, Y: -3, Z: 0)` (near the bottom center).
   - **CRITICAL**: In the Inspector, change it\
2. **Add Physics**:
   - Add a `BoxCollider2D` component.
   - **Check the `Is Trigger` box.** Size it to appropriately cover your catching zone.
   - Add a `Rigidbody2D`. Set Body Type to `Kinematic`.
3. **Add Visuals**:
   - Right-click `PlayerHand` > **2D Object > Sprite**. Name it `HandVisual`.
   - Assign a Hand or Basket sprite to the Sprite Renderer.
4. **Configure HandController**:
   - Add the `HandController` script to `PlayerHand`.
   - Drag the `BoxCollider2D` into the `Catch Collider` slot.
   - Drag the `HandVisual` child into the `Hand Visual` slot.
   - Set the `Active Duration` (e.g., 0.5 seconds). This is the timing window the player has to catch a fruit after pressing the button.

## Phase 4: Falling Fruits & Spawner

1. **Create the Fruit Prefab**:
   - Hierarchy > **2D Object > Sprite**. Name it `Fruit`.
   - Assign a fruit or monster head sprite (from Fantazia).
   - Add a `BoxCollider2D` and check **Is Trigger**.
   - Add the `FallingFruit` script.
   - Drag some particle prefabs from `Assets/Epic Toon FX/Prefabs/` into the `Catch Effect Prefab` and `Miss Effect Prefab` slots for juicy feedback.
   - Drag `Fruit` from the Hierarchy directly into your `Assets/[Your Games]/Fruit Catcher/` folder to save it as a Prefab. Delete it from the Hierarchy.
2. **Create the Spawner**:
   - Hierarchy > **Create Empty**. Name it `FruitSpawner`.
   - Set its position to `(X: 0, Y: 6, Z: 0)` (above the camera view).
   - Add the `FruitSpawner` script to it.
3. **Link Spawner to Manager**:
   - Select `GameManagers`.
   - In the `FruitGameManager` component, expand `Fruit Prefabs`, set size to 1 (or more), and drag your new `Fruit` prefab into the slot.
   - Adjust `Spawn Interval`, `Spawn Min X`, and `Spawn Max X` as desired.

## Playtesting

When you press Play:

1. The Main Menu should animate in. Click "Start".
2. Fruits will begin falling from the sky based on the manager's interval.
3. The Player Hand will sit idly.
4. When you press the physical sensor (mapped to `LiftLeftPlatform` or `Spacebar`), the hand scales up, turns green, and its collider activates for `0.5s`.
5. If the fruit hits the active collider, you score and trigger an effect! If it falls below `Y: -10`, you lose a health point.
