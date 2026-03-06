# C# Learning Game: Castle Adventure

An interactive 2D educational RPG developed with Godot Engine 4 and C#. This project is designed to teach C# programming fundamentals through immersive gameplay and practical problem-solving.

# Overview

In Castle Adventure, players take on the role of a knight trapped in a mysterious castle. To escape, the player must explore rooms, study theoretical materials, and solve C# programming challenges. The game bridges the gap between theory and practice by requiring players to apply what they've learned to unlock the path forward.

**Tech Stack**
Engine: Godot 4.x 

Language: C# (.NET 6.0)

Architecture: node-based scene composition, interface-driven interaction

Patterns: observer pattern (signals), singleton (autoload), polymorphism

# Key features and implementation

**1. Interactive quest system (marker and logic)**

The core of the game is a Dynamic Marker System. Unlike static tutorials, the game uses a custom IPrompt interface implemented by all interactive objects (Signs, Doors, Questions).

- Sequential learning: the marker points to the next task and only advances once the BecameNext() condition returns true.

- Polymorphism: this allows the same marker logic to handle a simple dialogue sign or a complex code-validation door seamlessly.

**2. Physics-based kinematics**

- Movement: implemented using CharacterBody2D with vector-based velocity calculations.

- Vector math: uses vector normalization to ensure consistent movement speed during diagonal traversal.

- Collision handling: integrated Godot’s physics engine with MoveAndSlide() for smooth environmental interaction.

**3. Advanced input validation**

The game features three distinct types of technical challenges:

- Code injection (Fill-in-the-blanks): logic that scans user input for specific C# keywords and validates syntax.

- Manual console input: a system that captures string input and compares it against expected results while managing player state (locking movement during input).

- Multiple choice: an event-driven system providing instant visual and auditory feedback via Godot Signals.

# Project structure

Level 1: focuses on imperative programming (Variables, Data Types, If/Else).

Level 2: focuses on object-oriented programming (Loops, Classes, Methods, Data handling).

Scripts: organized C# classes (e.g., Movement.cs, Marker.cs, Sign.cs).

# Setup & Installation

**Prerequisites**
- .NET SDK 6.0+

- Godot Engine 4.x (.NET Version) 

**Running the project**
- Clone the repository: git clone https://github.com/polinayerovenko/Godot-CSharp-Game.git
  
- Open in Godot
  
- Launch Godot and import the project.godot file
  
- Build solution: click the "Build" button in the top-right corner of the Godot editor to compile the C# scripts

- Play: press F5 to start the game

**Controls**
- WASD: move the Knight

- Shift: sprint

- E: interact with objects (Signs, Doors, Panels)

- Mouse: navigate UI and click buttons

- Manual console input: a system that captures string input and compares it against expected results while managing player state (locking movement during input).

- Multiple choice: An event-driven system providing instant visual and auditory feedback via Godot Signals.
