# Dragon Run - Unity Assembly Instructions

This guide walks you through assembling the new "Dragon Run" game. The gameplay features a dragon character that runs forward infinitely.
The difficulty increases based on the distance walked.

- You face tall walls with upper/lower gaps (you must fly or walk down to pass them).
- You face large monsters that cover the entire screen height. You must blast them with Fire Breath.

## Phase 1: Scene & Manager Setup

1. **Create the Scene**:
   - `Assets/[Your Games]/Dragon Run/` > right-click > **Create > Scene**. Name it `DragonRunScene`.
2. **Setup the Managers Object**:
   - Create an Empty GameObject named `GameManagers`.
   - Attach `DragonGameManager.cs` to it.
   - Attach the existing `GameInputManager.cs` (to bridge PLAKOD inputs).
   - Ensure the actions `MoveUp` (fly) and `MoveRight` (fire breath) are mapped (Check keyboard overrides like Spacebar and LeftControl).
3. **Setup Camera & CameraShake**:
   - Select `Main Camera`. Change its background color or attach a scrolling 2D Sprite.
   - Attach `CameraShake` script to it.

## Phase 2: Create the Dragon Player

1. **Create Player Object**:
   - Create an Empty GameObject named `DragonPlayer`.
   - Set Position to `X: -5, Y: -3, Z: 0` (left side of the screen).
   - In the Inspector, change its **Tag** to `Player`.
2. **Add Physics**:
   - Add a `BoxCollider2D`. Check **Is Trigger** and size it around the dragon's body.
   - Add a `Rigidbody2D`. Set Body Type to `Kinematic` and freeze Z rotation.
3. **Add Visuals & Scripts**:
   - Add the `DragonController` script to it.
   - Right-click `DragonPlayer` > **2D Object > Sprite**. Name it `Visuals`. Assign a dragon graphic. Drag this child object into the `Dragon Visual` slot on the script.
   - Create another Empty child object named `FireSpawnPoint`. Move it slightly to the right of the dragon's mouth `(X: 1, Y: 0)`. Drag it into the `Fire Spawn Point` slot in the script.

## Phase 3: Create the Fire Breath Prefab

1. **Create Fire Object**:
   - Create a **2D Object > Sprite** named `FireBreath`. Tag it as `Untagged` initially.
   - Add a `BoxCollider2D` and check **Is Trigger**.
   - Add the `FireBreath.cs` script.
   - Under `Assets/Epic Toon FX/Prefabs/`, grab a fire/explosion effect and drag it into the `Impact Effect Prefab` slot.
2. **Save to Prefabs**:
   - Drag `FireBreath` from Hierarchy into your `Assets/[Your Games]/Dragon Run/` folder to save it as a Prefab. Delete it from the scene.
   - Return to your `DragonPlayer`, and populate the `Fire Breath Prefab` slot on the `DragonController`.

## Phase 4: Create Obstacles & Monsters

1. **Create the Walls**:
   - **Wall Type 1 (Hole at Bottom):** Create a Sprite named `Wall_High`. Set position so the wall hangs from the ceiling. Tag it **Obstacle**. Add a `BoxCollider2D` covering the solid part. Mark `Is Trigger`. Save as a Prefab.
   - **Wall Type 2 (Hole at Top):** Create a Sprite named `Wall_Low`. Set position from the floor upwards. Tag it **Obstacle**. Add a `BoxCollider2D` (Is Trigger). Save as a Prefab.
2. **Create the Monster**:
   - Create a Sprite named `Monster_Big`. Assign a huge monster sprite (from Fantazia maybe) that spans the screen's Y-height.
   - Change its **Tag** to **Enemy**.
   - Add a `BoxCollider2D` (Is Trigger). Save as a Prefab.
   - (_Note: Any object spawned by the Spawner will automatically receive the `ScrollingObject` script to move it left_).

## Phase 5: Spawner Setup

1. **Create the Spawner**:
   - Create an Empty GameObject named `ObstacleSpawner`.
   - Position it far to the right of the screen (e.g., `X: 12, Y: 0`).
   - Attach the `ObstacleSpawner.cs` script.
2. **Populate Spawner Settings**:
   - Set the size of `Wall Prefabs` to 2. Drag in `Wall_High` and `Wall_Low`.
   - Set size of `Monster Prefabs` to 1. Drag in `Monster_Big`.

## Phase 6: User Interface (UI) Setup

1. **Create Canvas**: Right-click Hierarchy > **UI > Canvas**.
2. **Create Main Menu Panel**:
   - Panel named `MainMenuPanel`. Add a start button.
   - Link the button via the Inspector's OnClick() to `GameManagers.DragonGameManager.StartGame`.
3. **Create HUD**:
   - Add a TextMeshPro text named `DistanceText` (Top left).
   - Add a UI Image named `HealthFill_Parent` and a child filled `HealthFill` Image for the health bar.
4. **Create Game Over Panel**:
   - Panel named `GameOverPanel`. Add a "Game Over" text, a `FinalScoreText`, and a "Retry" button linked to `DragonGameManager.RestartGame`.
5. **Link to Manager**:
   - Drag all these UI pieces into the corresponding slots on `GameManagers` -> `DragonGameManager`.

## Testing the Game

Press **Play** and hit Start. The dragon will spawn at the "walk down" bottom height.

- **Flight**: Press or hold the configured flight button (MoveUp or Space) to ascend instantly. Release to descend.
- **Fire**: Press the fire button (MoveRight or LeftControl) to blast a `FireBreath` object forward. You must release and tap again to cast repeatedly.
- **Speed**: As time progresses, walls/monsters move faster and spawn more rapidly. The Distance text displays your increasing score based on this dynamically increasing speed.
