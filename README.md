# CSC443\_Final



Final Project: Infinite Runner Extension

Course: CSC 443    



Leen  Abou Farraj

20231012

\---



Project Overview

This project is an extension of the Subway Surfers-style 3D Infinite Runner framework built in class. The base architecture has been expanded into a fully functional, polished, and loopable gameplay experience satisfying all core rubric metrics and incorporating advanced custom extensions.



\---



Implemented Features



1\. Required Core Features

* Collision \& Game Over
* Restart Mechanic
* Score HUD



2\. Chosen Extensions

* Extension B - Main Menu: Formulated as a standalone, dedicated scene (Index 0 in Build Settings). Contains a structured UI complete with custom title cards and an interactive (play) button that cleanly manages scene streaming.
* Extension C - Pause Menu: Controlled globally via the Escape (Esc) key utilizing modern asynchronous tracking from the Unity New Input System. Pressing escape dynamically checks states, activates a dedicated `PausePanel` freeze-frame overlay, and provides fluid options to Resume gameplay or Quit safely back to the Main Menu.



3\. Gameplay / Movement Modifications

* Train-Walking \& Double Jump: Engineered advanced 3D raycasting downward from the player's waist axis to dynamically detect geometry allocated to the `Ground` physics layer. Players can double-tap "W" to climb, run, and balance smoothly along moving train roofs while retaining lethal vulnerability if they crash directly into front-facing bumpers.



\---



Structural Architecture \& Optimizations

* Procedural Level Splicing: Level layout uses optimized socket-matching rules to stitch tracking elements. The `LevelGenerator` has been fortified to explicitly stream `Element 0` (the clean, obstacle-free template runway) as the mandatory starting piece to eliminate immediate spawn-blocking bugs.
* Clean Repository Pipeline: Maintained structural sanitation standards. The repository is optimized with a standard Unity `.gitignore` layout tracking only core project data configurations (`Assets/`, `Packages/`, `ProjectSettings/`), explicitly redacting volatile storage layers (`Library/`, `Logs/`, `Temp/`, `Build/`).



\---

No Known Bugs

