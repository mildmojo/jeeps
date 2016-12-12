# Jeep Wranglers

One-button tactical racing game. Get your battery-powered toy jeep out before
anyone else. Button to jump.

Failed entry for Ludum Dare 37, "One Room". Built using Unity 5.4 for Linux.

## Design

### Aesthetic

Single-screen arcade game like Pac-Man or Donkey Kong. A little more advanced
than Tiger LCD games, but with the same sort of static positioning for screen
elements. Pastel-ish colors. Maybe ragtime music or something cheery and upbeat.

See the [vector spritesheet](Assets/images/sprites.svg) for a game board mockup.
It doesn't render on GitHub, you have to open it in Inkscape.

### Setup

Players start at the bottom of the board and attempt to race to the top. Jeeps
automatically drive forward. Pressing a button makes the jeep jump over the
next tile.

### Tiles

Tiles can contain special icons. When a jeep reaches the center of a tile, it
triggers a behavior indicated by the icon.

- Arrows: Change the jeep's direction.
- Corkscrew arrows: Rotate the 8 tiles surrounding the corkscrew tile in the
  direction indicated.
- Monster: Spawns a monster that runs around the board wreaking havoc.
- Umbrella: Teleport player to another random tile nearby.
- Ice cube: Bursts out in 4 directions, replacing a bunch of tiles with ice. Ice
  might spin players, but otherwise has no effect.
- Moon: Darkens level for everyone with light around jeeps (headlights?) like
  TowerFall.
- Fast-forward: Directional, skips player in direction of arrows faster than
  normal.

### Joining the game

Like a few of my other games, one-button joining follows these rules. Before the
game begins:

- When a player holds any button (keyboard, mouse, or gamepad), they join the
  game, the engine moves to setup mode, and a timer counts down to the start of
  the game.
- Joining the game randomizes your jeep and player hair color.
- Releasing the button removes the player from the game. If they're the last
  player left, the countdown timer stops and the game returns to attract mode.
- The countdown timer restarts each time a player joins the game.
- When the countdown timer finishes, the game state advances to gameplay.

### Movement

Players move at a constant rate, essentially lerping from one tile center to the
next at the same speed.

As a player aid, I figured it would help to highlight a player's current path.
First idea was to use a long-lived trail renderer on an object that traces the
path following the same logic as the player.

### Ending the game

There's either an overall game timer or the first person to finish triggers
a countdown for everyone else.

## Current state

There's a player join system with countdown, but releasing your button doesn't
leave the game.

Once the game begins, the GameManager will animate players based on their
current heading. Nothing currently affects heading, and there's no check for
the goal state at the top of the board.

The game doesn't track currently-active players, and there's only one player
object in the scene. Scene should have 6 player objects that (de)activate as
players join and leave during setup.

Tile types are not implemented. I didn't find a good way to represent tiles,
which should have a base layer sprite, an icon overlay sprite, and some code
that dictates behavior based on the tile type (as outlined above). The board is
represented in GameManager as a one-dimensional `List<Tile>` in row order.

## Useful stuff

- The [TwoDee](Assets/scripts/TwoDee.cs) class wraps a `GameObject` and uses its
  renderer to provide nice width/height/x/y/left/top values. I wrote it for
  "The Bombay Intervention" but didn't use it effectively here.
- The [ShuffleDeck](Assets/scripts/ShuffleDeck.cs) class. My favorite class,
  always useful. Wraps a collection and treats it like a deck of cards you can
  shuffle and draw from.
- The [AllButtons](Assets/scripts/AllButtons.cs) and [AnyInput](Assets/scripts/AnyInput.cs)
  classes. Together, you can poll indiscriminately for any keyboard/mouse/gamepad
  button (300+ of them!), ask if any of those were pressed in the last frame,
  and get a list of those KeyCodes for later use. Used in the player join system
  to let players use absolutely any button for control. `AnyInput` needs to be
  attached to a `GameObject` so its `Update` can run for polling.
