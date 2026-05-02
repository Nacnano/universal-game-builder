# PROJECT GUIDELINES: PLAKOD REHABILITATION GAME

## 1. Project Overview & Objective

**The Goal:** Develop a "Video Game-Assisted Physical Rehabilitation" prototype consisting of two synchronized parts:

1. **Hardware (Rehab Device):** A physical device built using provided materials and sensors.
2. **Software (Video Game):** A custom video game built in **Unity** that responds to the hardware inputs.

**The Output:** A functional prototype designed specifically to aid in the physical and cognitive rehabilitation of stroke patients.

## 2. Target Audience & Medical Constraints

**Target User:** Stroke Patients.

- **Physical Limitations:** Patients often suffer from partial or permanent paralysis, disability, or muscle weakness (hemiparesis). The game must accommodate limited mobility (some may even be bedridden).
- **Cognitive Impairments:** Stroke patients often have reduced cognitive function.
  - _Constraint:_ Tasks must be easily understandable.
  - _Progression:_ The game must allow for progressive difficulty (e.g., starting with single-digit math or simple color matching before moving to complex problems).
- **Rule of "1 Game = 1 Task":** The game must focus on **ONE specific physical rehabilitation posture or movement** (e.g., targeting exclusively the Upper Limb OR the Lower Limb). Do not try to make a game that does everything.

## 3. Software Requirements (Unity)

- **Engine:** MUST be developed entirely in **Unity**.
- **Creative Freedom:** You have complete freedom to modify the provided base template.
  - You can write code from scratch.
  - You can change all mechanics.
  - You can use custom 2D or 3D assets, levels, backgrounds, and items.
- **Input Mapping:** The game must be designed to receive inputs _exclusively_ from the physical sensors attached to the Plakod Controller (which will likely translate physical signals into standard keyboard/controller inputs within Unity).

## 4. Hardware Integration (The Physical Device)

The physical device will be built on Day 2. Your Unity game must be conceptualized to work with the following physical constraints:

- **Controller Hub:** "PLAKOD Controller" acts as the bridge. It takes signals from the physical device and sends them as controls to the Unity game.
- **Available Sensors & Switches (Input Methods):**
  1.  IR Infrared Obstacle Avoidance Sensor (triggers when an object/body part is near).
  2.  LDR Photoresistor Sensor (triggers based on light/shadow).
  3.  Angle Tilt Sensor (triggers based on the angle/rotation of the device).
  4.  Micro Limit Switch (triggers upon physical press/click).
  5.  Conductive Copper Foil Tape (acts as custom touch/connection switches).
- **Building Materials:** The physical form of the controller will be constructed using PVC tubes, Cardboard, and Foam blocks.
- _Note for AI coding:_ The game's input manager must be incredibly simple, relying on binary triggers (On/Off) or simple analog inputs derived from these specific sensors.

## 5. Evaluation Criteria (How to Win)

Your game and hardware combo will be judged by **Physical Therapists and Real Patients**. To win, the project must score high in the following three categories:

**A. Feasibility (Practicality & Safety)**

- _Safety (ความปลอดภัย):_ The physical movement required to play the game must not injure the patient.
- _Anatomical Accuracy (ความถูกต้องทางกายภาพ):_ The movement must be a medically recognized and effective rehabilitation exercise.
- _Practicality (การใช้งานได้จริง):_ The system must actually work and be usable by someone with stroke limitations.

**B. Desirability (User Engagement)**

- _Motivation (แรงจูงใจในการใช้งาน):_ The game loop must incentivize the patient to perform the painful or boring rehab movements.
- _Fun/Replayability (ความสนุก/อยากใช้ต่อ):_ The game must be enjoyable enough that the patient _wants_ to play it again.
- _Daily Life Fit (เหมาะกับใช้ในชีวิตประจำวัน):_ The system should make sense for routine home or clinic use.

**C. Novelty (Innovation)**

- _Creativity (ความคิดสร้างสรรค์):_ Clever use of the provided sensors and game mechanics.
- _Originality (ความแปลกใหม่):_ A fresh take on rehab that hasn't been seen in standard physical therapy.
- _Differentiation (ความแตกต่าง):_ Standing out from other hackathon teams.

## 6. Final Submission Requirements

The AI agent and your team must prepare the following for the Day 2 deadline (before 17:30):

1.  **Software Build:** The final exported game file (Game Developed in Unity).
2.  **Hardware Proof:** The physical device with sensors attached.
3.  **Video Pitch:** A video demonstrating the prototype in action (Testing phase), explaining how it works and solves the problem.
4.  **Upload:** The video must be uploaded to YouTube as "Unlisted".
5.  **Submission:** Scan the final QR code, fill out the form, and submit the game file and video link.
