# CMPM 121 Framework

Game project worked over the course of a quarter for the class of CMPM 121. As students, were given a foundation of a made game and told to improve and expand upon it. We added multiple elements, such as spells, spell modifiers, relics (universal modifiers), rpg systems, and basic Game AI node pathfinding.

## Key Systems Built

### Spells, Modifiers, and Relics
- Modular spells using a wrapper system for dynamic values, only needed values which can change calculated using wrappers.
- Template foundation for spells, modifiers, and relics
- Builder classes for spells, modifiers and relics, to minimize repeating code
### Projectile System
- Allows for different movement paths, size changes, delays, bursts, spread, and tracking
- Separate system to any spell modifier or relic, keeping both encapsulation and decoupling standards high
### JSON Parsing
- Parsing system due to requirements of the assignment, using a template class to read, and track data down into data classes to later be extracted, keeping a level of abstraction without needing to parse again during runtime
### AI Navigation
- Due to limited time, stuck with a basic search and strict node placement
- All node connections are made during launch, using line of sight or casting to find suitable connections
### Core Architecture
- A central game manager, along with sub managers for enemy spawning, nav path finding, rpg system, and UI elements
- Event manager which handles all signals or actions / events, making sure any call must first travel through the manager, helping to keep track of all data moving throughout the program for debugging.

# Running the Project
Currently the only way is through downloading and creating an executable

# Framework for CMPM 121

This framework is for the class CMPM 121 - Game Development Patterns. It was developed in Unity version 6000.0.23f, but should also work in other Unity versions. 

## Artwork

All art used in this framework was released under CC-0. 

Spell and relic icons:
https://opengameart.org/content/dungeon-crawl-32x32-tiles

Roguelike Dungeon tiles:
https://opengameart.org/content/roguelike-caves-dungeons-pack
https://kenney.nl/assets/roguelike-caves-dungeons

UI:
https://kenney.nl/assets/ui-pack-pixel-adventure

Enemy sprites:
https://opengameart.org/content/tiny-creatures

Arcane bolt projectile:
https://opengameart.org/content/arcane-magic-effect
