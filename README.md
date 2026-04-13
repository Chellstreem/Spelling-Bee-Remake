Spelling Bee — Learning Edition

Overview
--------
This repository contains a learning-oriented prototype of a "Spelling Bee" style mini-game built with Unity. The project was created as an educational exercise to explore game mechanics, basic architecture patterns, and Unity workflow. It intentionally preserves early design decisions and simplified implementations for study and improvement.

Project Goals
-------------
- Prototype core gameplay and UX for a word-building mini-game.
- Demonstrate usage of Unity, Zenject (dependency injection), and common game patterns.
- Provide a readable codebase for learning, experimentation, and refactoring practice.

Technologies & Dependencies
---------------------------
- Unity Editor: 2022.3.33f1 (project configured for Unity 2022.3 LTS).
- Language: C# (scripts under `Assets/Scripts`).
- Packages: TextMeshPro, Input System, Zenject, and other packages listed in `Packages/manifest.json`.

Repository Structure
--------------------
- `Assets/Scenes/` — project scenes (menu, intro, main stage).
- `Assets/Scripts/` — core gameplay code: word controllers, state management, spawners, UI, input.
- `Assets/Prefabs/`, `Assets/Sprites/`, `Assets/Sounds/` — assets and prefabs used by the game.
- `ProjectSettings/` — Unity project settings (editor version, player settings, etc.).

How to Open and Run
-------------------
1. Install Unity 2022.3 LTS (recommended to match the version in `ProjectSettings/ProjectVersion.txt`).
2. Open the project folder in Unity Hub and let Unity restore packages and import assets.
3. Open a scene from `Assets/Scenes/` (for example, the Main Stage) and press Play.

Architecture Notes
------------------
- Zenject is used to separate dependencies and make systems easier to test and replace.
- Code is organized by responsibility: input, word logic, units/AI, UI, and VFX.
- As a learning project the code contains simplifications and areas that would benefit from refactoring and unit tests.

Limitations
-----------
This project is not production-ready. Many systems are intentionally simplified for teaching purposes and can be optimized or redesigned for robustness and performance.

Contact
-------
If you have questions or want to discuss improvements, open an issue in the repository.

Thanks for checking out the project — learn, experiment, and improve!
