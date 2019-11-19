# Dasik AI
AI Behaviour tree for Unity

## About
- Simple to use
- Visual debug
- Extendable
- Fast

### Wiki
* [Examples branch](https://github.com/dasik/DasikAI/tree/Example) 

### Installing with Unity Package Manager
*(Requires Unity version 2018.3.0b7  or above)*

To install this project as a [Git dependency](https://docs.unity3d.com/Manual/upm-git.html) using the Unity Package Manager,
add the following line to your project's `manifest.json`:

```
"com.dasik.ai": "https://github.com/Dasik/DasikAI.git",
"com.github.siccity.xnode": "https://github.com/siccity/xNode.git"
```

You will need to have Git installed and available in your system's PATH.

### Installing without Unity Package Manager

Or you can just copy the folder 'DasikAI' in your asset scripts folder. 

### Example

![Behavior Tree.png](/Images/BehaviorTree1.png)

What is shown here

1. State Idle. If distance to Player between 0 and 20 go to 2.
2. Follow. If distance between 0 and 10 attack player. Else if distance between 10 and 40 Move to player. Else start searching.
3. Attack. If distance between 0 and 10 move around player and attack. Else Follow.
4. Search. If an Agent loses a player, during next 15 seconds his move to last player position. If distance between 0 and 20 Folow. If time is elapsed go to idle

Description is very simplified. In example it's have some other parameters and addtitional logic.