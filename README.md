  ## AsteroidsArcade
  ## Late 70s Atari game clone

  ## Overview.
  ###### This project copies basic Asteroid Arcade gameplay elements.
  ###### Esc - quit
  ###### Arrows / WAD - move and rotate (no backwards move)
  ###### LMB / space - shoot
  ###### 4 lives per game. For every 1000pts a UFO comes. Rewards for asteroids: large - 20pts, medium - 50pts, small - 100pts; UFO - 200pts.
  ###### No leveling, game ends with all lives beaten.

  ## Classes.
  #### GameManager - 
entry point with the only Awake() method in the project. Handles game state, reloads project on every scene reload.
  #### SceneHelper - 
keeps pixels to world units Math, spaceship game object link and main camera links. Static.
  #### SpaceshipController - 
handles controls, produces bullets, die/respawn logic, calls Player.LoseLife(). Publics for editor: spaceship velocity and torque multipliers, bullet speed and lifetime. Is teleportable.
  #### AsteroidController - 
handles asteroids collisions, movement, produces children after death. Publics: velocity bounds, max torque. Is teleportable, is exploadable.
  #### AsteroidHelper - 
auxilary static class. Basic asteroid info.
  #### AsteroidBuilder - 
produces asteroids of random types and sizes (stages in project names), keeps duration before next spawn and decreases it with PointsOverSecond speed.
  #### UIController - 
keeps score and remainig lives in top left screen corner, has game over panel. Static
  #### GameState, PlayerState classes -
more likely structs. Only purpose - keeping whether the player is alive and the game is ongoing or not.
  #### Player - 
keeps info about it's lives and points. Every 1000pts calls a UFO to spawn. Keeps immortality duration. Static.
  #### UfoController - 
produces and keeps settings of UFOs. Has 2 phases in player pursuit - horizontal and straight. Is teleportable, is exploadable.
  #### ResourcesLoader - 
keeps singletons of resources that are needed to be loaded on fly, e.g. asteroids, explosions or spaceship different texture. Static.
  #### TeleportableBehaviour - 
calculates and runs teleportation logic for onscreen objects. Moves them to the opposite side of the screen. Owners: SpaceshipController, AsteroidsController, UfoController.
  #### ExploadableBehaviour - 
checks if the collision should destroy the asset and if needed does it. Owners: UfoController, AsteroidController.

  #### Project has primitive sound effects for hitting the player, destroying enemy, UFO flying, shooting and game over.
  #### Expected game behaviour: any player collision = death, enemies do not collide each other, enemy and laser collision = enemy death, UFOs change phases of pursuit, after game over enemies are not teleportable anymore.
