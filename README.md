**Game Title:** 
Castaway Riches

**Brief Elevator Pitch Describing Game:**
Castaway Riches is a fishing-themed casual arcade game that offers a twist from traditional fishing games; by incorporating various unique mechanics, varieties of fish, and an endless mode, Castaway Riches aims to deliver a continuously engaging experience to players.

**Game Instructions:**
- In the Unity Hierarchy window, make sure that only the Start Scene is loaded in. In the Start Scene, you can select between the default mode and the swinging mode, which has the fishing line swinging back and forth. Once a mode has been selected, click the “Start Game” button to begin playing the game.
- When the game starts, the player acts as the fisherman. They must move around and gather enough fish, which generates them points, before the timer runs out. If the timer runs out, it’s game over and the player has the option to restart. If the player makes it past the point threshold for their current level, they progress onto the next level where new, more difficult-to-catch fish are available.
- Players should avoid sharks, as they will lose points.
- During gameplay, the player can use “A” and “D” to move left and right and use “Space” to extend the fishing line and hook.
- This game is best played on a computer so that the WASD and Space keys can be used.

_For the sake of game testing, you can press the J key to skip through the levels._

**Available Content:**

_Fish_
- Various types of fish are offered as the player progresses through the levels. There are 7 different kinds of fish, each with various movement behaviors and patterns to provide a challenge to the player as they complete each level. There is low variability in the fish that can be caught at the beginning of the game, but after playing a few minutes, there should be plenty of fish to reel in.
- If players cannot collect the fish in time, the fish will eventually swim off of the screen and are no longer able to be fished.
- Besides the seven types of fish, there are also sharks that will be randomly generated. Players are deducted points for hitting the shark, with higher levels deducting more points, and, unlike the typical fish, the shark will not disappear after touching the hook. Players should therefore always be careful to avoid the shark.
- Fish and sharks that are spawned vary in size.

_Power-ups_
- Various types of temporary power-ups are also available to aid the player in progressing through the game––a power-up that increases the time left on the clock, an increase in boat movement speed, and an increase in reeling speed. 
- The boat movement speed and reeling speed power-ups last 5 seconds and double the original speeds of the player.
- The power-up that increases the time left increases the clock by 5 seconds.
- Power-ups spawn at random intervals and every time one spawns, it is randomly selected from the 3 that are currently available.
- If players forget what each power-up does, there is a menu at the top center of the screen that they can click on to remind themselves. They can click on the red X button to close the menu.
- If players don’t collect the power-ups, they are automatically deleted after a set amount of time (5 seconds).
When the increase in boat movement speed and reeling speed power-ups are active, an indicator appears near the player’s head to show the ongoing buff. Once the power-up expires, the indicator disappears.
- If a player collects two of the same power-ups simultaneously, they will only retain the buff that they would have gotten from collecting one power-up. This was purposefully done for the sake of balancing the game.

_Points, Levels, Screens_
- There are an infinite amount of levels, with the point threshold for each level increasing as the player progresses. Each level lasts 30 seconds.
- Each fish provides a differing amount of points, with fish more difficult to catch generating a greater amount of points for the player.
- When players begin the game, they are met with a start screen. They can select a gamemode and begin playing the game after pressing the start button.
- If the game ends because the player did not reach the level’s point threshold in time, a restart game screen pops up where they can retry by hitting the try again button, or go back to the start screen by hitting the main menu button.

**Lessons Learned:**
- Game development involves coding just like any other coding domain would, meaning that code organization is vital when it comes to working on large projects. With so many moving parts, components, assets, screens, etc. to deal with, it’s easy to get lost on where a feature was implemented. Proper naming conventions and comments documenting progress are of utmost importance.
- In game development, the end goal is to make the game fun for players; a game cannot be fun if there is improper balancing. To ensure players have both a satisfying and challenging experience, it’s necessary to constantly iterate on the mechanics and interactions within the game.
- Gathering feedback is essential when wanting to make improvements in overall player engagement and satisfaction. Prospective players bring about new ideas that original game developers may not come up with, many of which can enhance the gameplay experience.
