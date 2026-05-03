# Dragon Run - Beginner's Unity Assembly Guide

Welcome to Unity! If you are new to the engine, don't worry. This guide explains every step in detail.

### Quick Glossary before we start:

- **Project Window (usually at the bottom):** This is your file browser. It shows all your folders, scripts, and images.
- **Hierarchy (usually on the left):** This is a list of everything currently _inside_ your active game level (Scene).
- **Inspector (usually on the right):** When you click an object in the Hierarchy, the Inspector shows all of its details, settings, and "Components" (like scripts or physics).
- **Scene View (middle):** Where you visually place and move objects.
- **Game View (middle):** What the player will actually see.

---

## Phase 1: Scene & Manager Setup

1. **Create the Game File (Scene):**
   - Go to your **Project Window** at the bottom. Open `Assets` > `[Your Games]` > `Dragon Run`.
   - Right-click in the empty space > **Create** > **Scene**. Name it `DragonRunScene`.
   - Double-click `DragonRunScene` to open it. (Your Hierarchy should now be empty except for a Main Camera).
2. **Setup the Infinite Scrolling Environment (Parallax):**
   - **The Background (Moves slowly):**
     - Right-click Hierarchy > **2D Object > Sprite**. Name it `Background`. Pick an image (like a sky or forest).
     - Set **Order in Layer** to `-10` so it stays in the back. Scale it up so it fully covers the camera.
     - Move it mathematically to exactly `X: 0, Y: 0`.
     - Click **Add Component** > type `AutoParallax` and hit Enter. Set **Speed Multiplier** to `0.2` (it will move at 20% speed of the obstacles, giving a 3D effect!).
   - **The Ground (Moves fast):**
     - Right-click Hierarchy > **2D Object > Sprite**. Name it `Ground`. Pick a dirt/floor image.
     - Set **Order in Layer** to `5` so it stays in front of everything. Move it to the bottom of the camera (e.g. `Y: -4.5`). Scale it wide enough to cross the whole screen.
     - Make sure its starting `X` position is `0`.
     - Click **Add Component** > type `AutoParallax` and hit Enter. Set **Speed Multiplier** to `1.0` (it will zoom by exactly as fast as the walls!).
3. **Setup the Managers Object (The Brains):**
   - Right-click in the empty space of your **Hierarchy** window and click **Create Empty**.
   - Click on this new "GameObject" and look at your **Inspector** on the right. Rename it to `GameManagers` at the very top.
   - Look down in the Inspector and click the **Add Component** button. Type `DragonGameManager` and press Enter to add our script.
   - Click **Add Component** again. Type `GameInputManager` and press Enter. This tells the game to listen to the PLAKOD physical sensors!
   - In the `GameInputManager` script in the Inspector, find the "Bindings" list. Make sure you map `MoveUp` to your physical button for flying, and `MoveRight` to your physical button for firing. (For testing on a keyboard, you can add `Space` and `LeftControl`).
4. **Setup Camera Shake:**
   - Click `Main Camera` in the Hierarchy.
   - In the Inspector, click **Add Component**, type `CameraShake`, and press Enter.

---

## Phase 2: Create the Dragon Player

1. **Create the Player Object:**
   - Right-click in the Hierarchy > **Create Empty**. Rename it to `DragonPlayer`.
   - In the Inspector, find the **Transform** section. Set Position to `X: -5, Y: -3, Z: 0` (this moves the player to the left side of the screen).
   - **CRITICAL TAGGING:** At the top of the Inspector, find the **Tag** dropdown (it usually says "Untagged"). Click it and choose **Player**.
2. **Add Physics (Colliders):**
   - Click `DragonPlayer`, then in the Inspector click **Add Component**. Search for `BoxCollider2D`.
   - Find the little checkbox labeled **Is Trigger** and **Check it**.
   - Click **Add Component** again. Search for `Rigidbody2D`.
   - Under the Rigidbody2D settings, find **Body Type** and change it from "Dynamic" to **Kinematic** (this stops gravity from making our dragon fall off the screen).
3. **Add the Script & Visuals:**
   - Add the `DragonController` script via the Add Component button.
   - Now, right-click `DragonPlayer` in the Hierarchy and select **Create Empty**. Rename this to `Visuals`.
   - **Adding your Spine Dragon:** Open `Assets` > `Fantazia Animated 2D Monsters` > `Source_Animations` > `Monster_27_28_29_Dragon`. Find the asset named `monster_08_SkeletonData` (it looks like a little Spine ghost icon).
   - Drag `monster_08_SkeletonData` into your Scene View directly onto your `Visuals` object. A popup might appear asking what to create—select **SkeletonAnimation**.
   - Make sure your Spine dragon faces the right way inside the `Visuals` container. You might need to change its **Scale X** to `-0.5` or `0.5` depending on how large you want it.
   - Right-click `DragonPlayer` again > **Create Empty**. Rename it to `FireSpawnPoint`. In its Transform, set position to `X: 2, Y: 0` (so fire comes out in front of the dragon).
   - Click `DragonPlayer` one last time to look at the script. Drag `Visuals` from the Hierarchy into the "Dragon Visual" slot **AND** the inside `Visuals` child holding the SkeletonAnimation into the "Dragon Skeleton" slot.
   - In the script you will see string slots for animations! Type exactly: `Walk` for walk, `Walk` for fly (since dragons flap while walking), `Attack` for attack, and `Dead` for dead.
   - Drag `FireSpawnPoint` into the "Fire Spawn Point" slot.

---

## Phase 3: Create the Fire Breath Prefab

_A "Prefab" is a template. We make an object once, save it as a file, and then the game can spawn hundreds of copies of it later._

1. **Create the Object:**
   - Right-click Hierarchy > **2D Object > Sprite**. Name it `FireBreath`.
   - Assign a fireball image in the Sprite Renderer.
   - Click **Add Component** > `BoxCollider2D` and check **Is Trigger**.
   - Click **Add Component** > `Rigidbody2D` and set Body Type to **Kinematic**.
   - Click **Add Component** > `FireBreath` (our script).
2. **Save as a Prefab:**
   - Click and drag `FireBreath` from the **Hierarchy** down into the **Project Window** (inside your Dragon Run folder). You will see it create a blue cube icon. This is your Prefab!
   - Now delete `FireBreath` from the Hierarchy (Right-click > Delete). We only needed the template.
   - **IMPORTANT:** Click your `DragonPlayer` again. Find the `DragonController` script, and drag your new blue `FireBreath` prefab from the Project window into the "Fire Breath Prefab" slot.
   - **HOW TO SHOOT:** When playing, press `Left Control` (or your PLAKOD `MoveRight` output) to blast fireballs!

---

## Phase 4: Create Obstacles & Monsters (More Prefabs)

1. **Make the Lower Wall (Obstacle on the Ground):**
   - Right-click Hierarchy > **2D Object > Sprite**. Name it `Wall_Low`. Assign a stalagmite, brick, or ground wall image.
   - **Tag it:** Change the Tag at the top of the Inspector to **Obstacle**. _(If Obstacle isn't there, click "Add Tag", hit the '+' icon, type "Obstacle", save, then click the Wall and assign it)._
   - Add a `BoxCollider2D` and check **Is Trigger**.
   - Drag `Wall_Low` into your Project Window to make it a Prefab. Delete it from the Hierarchy.
2. **Make the Upper Wall (Obstacle on the Ceiling):**
   - Right-click Hierarchy > **2D Object > Sprite**. Name it `Wall_High`. Assign a stalactite, vines, or ceiling image.
   - **Tag it:** Change the Tag to **Obstacle**.
   - Add a `BoxCollider2D` and check **Is Trigger**.
   - Drag `Wall_High` into your Project Window as a Prefab and delete it from the Hierarchy.
3. **Make the Giant Monster (Enemy):**
   - Right-click Hierarchy > **2D Object > Sprite**. Name it `Monster_Big`. Assign a scary monster image and use the Rect Tool (T) to scale it up really tall.
   - **Tag it:** Change the Tag to **Enemy**.
   - Add a `BoxCollider2D` and check **Is Trigger**.
   - Drag it into your Project folder to make a Prefab. Delete it from the Hierarchy.

---

## Phase 5: The Spawner Setup

1. **Create the Spawner:**
   - Right-click Hierarchy > **Create Empty**. Name it `ObstacleSpawner`.
   - Set its position to `X: 12, Y: 0, Z: 0` (off-screen to the right).
   - Click **Add Component** > `ObstacleSpawner`.
2. **Tell the spawner what to spawn:**
   - Look at the script in the Inspector. You will see three groups: "Lower Wall Prefabs", "Upper Wall Prefabs", and "Monster Prefabs".
   - Click the little arrow next to "Lower Wall Prefabs". Type "1" in the Size box and drag your blue `Wall_Low` prefab into the slot.
   - Click the little arrow next to "Upper Wall Prefabs". Type "1" in the Size box and drag your blue `Wall_High` prefab into the slot.
   - Click the little arrow next to "Monster Prefabs". Type "1" in the Size box and drag your blue `Monster_Big` prefab into the slot.
   - Set **Lower Wall Spawn Y** to `-2.5` (so it touches the ground) and **Upper Wall Spawn Y** to `2.5` (so it touches the ceiling). You can adjust these values based on your exact layout!

---

## Phase 6: The User Interface (UI)

1. **Create the Canvas:**
   - Right-click Hierarchy > **UI > Canvas**. (This is where all 2D UI lives).
2. **Create the HUD (Score and Health):**
   - Right-click Canvas > **UI > Text - TextMeshPro**. _(If asked, click "Import TMP Essentials"!)_
   - Rename it to `ScoreText`. Change the text to "Distance: 0" and move it to the top-left.
   - **PRO TIP (Anchoring text to corners):** Select `ScoreText`. In the Inspector, look for the **Rect Transform** component at the top. Click the "Anchor Presets" box (the square target icon showing center/middle). Hold down the **Shift** and **Alt** keys (or Shift + Option on Mac) and click the Top-Left box. This forces the text to stay stuck to that corner regardless of screen size! You can do the same for Health on the Top-Right.
   - Right-click Canvas > **UI > Image**. Rename it `HealthBackground`. Move it to the top-right. Set its color to black (to act as a border).
   - Right-click `HealthBackground` > **UI > Image**. Rename it `HealthFill`.
   - **CRITICAL STEP FOR HEALTH BAR:** In the Inspector for `HealthFill`, find the **Source Image** box (it usually says "None"). Click the tiny circle next to it and select **Background** or **UISprite** (default Unity squares).
   - _Once a Source Image is selected_, the **Image Type** dropdown will appear just below it! Change **Image Type** to **Filled**. Set **Fill Method** to **Horizontal** and change its color to Red.
3. **Make the Main Menu Panel:**
   - Right-click Canvas > **UI > Panel**. Name it `MainMenuPanel`.
     - **Appearance fix:** By default, Unity Panels are quite transparent. In the Inspector, click the **Color** box. Set it to **Black**, and then increase the **A (Alpha)** slider at the bottom to `200` or `230` to make it much darker and less transparent.
   - Right-click `MainMenuPanel` > **UI > Button - TextMeshPro**. Change the text child to say "Start".
   - Click the Button. Scroll down in the Inspector to "On Click ()". Click the **+** sign. Drag the `GameManagers` object from your Hierarchy into the empty object slot. Click the "No Function" dropdown > `DragonGameManager` > `StartGame()`.
4. **Make the Game Over Panel & Final Score:**
   - Right-click Canvas > **UI > Panel**. Name it `GameOverPanel`. Set its color to dark red.
   - Right-click `GameOverPanel` > **UI > Text - TextMeshPro**. Rename it `FinalScoreText`. Set the text to "Final Distance: 0m".
     - **Alignment fix:** In the Inspector, change **Pos Y** to `50` so it sits slightly above the center. In the TextMeshPro settings, set the **Alignment** to Center and Middle.
   - Right-click `GameOverPanel` > **UI > Button - TextMeshPro**. Rename it `RetryButton`. Change the text child to say "Retry".
     - **Alignment fix:** In the Inspector for the `RetryButton`, change **Pos Y** to `-50` so it sits below the score text instead of overlapping it.
   - Click `RetryButton`. Under "On Click ()", click the **+** sign. Drag `GameManagers` into the slot. Click "No Function" > `DragonGameManager` > `RestartGame()`.
   - **Important:** Click `GameOverPanel` and uncheck the little checkbox next to its name at the very top left of the Inspector to hide it when the game starts.
5. **Link the UI to the Brain:**
   - Click `GameManagers` in the Hierarchy.
   - Look at the `DragonGameManager` script in the Inspector. Drag your new UI elements into the empty slots:
     - **Main Menu Panel** -> `MainMenuPanel`
     - **Game Over Panel** -> `GameOverPanel`
     - **Score Text** -> `ScoreText`
     - **Final Score Text** -> `FinalScoreText`
     - **Health Fill** -> `HealthFill`

**You are done! Press the Play button (Top center of the screen) to test your game!**
