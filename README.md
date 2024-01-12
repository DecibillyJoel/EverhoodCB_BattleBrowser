# Battle Browser

A BepInEx plugin for the Everhood Custom Battles client that enhances the file browsing experience.

## Current Features

- Supports changing the battles that show up in the main menu of the Custom Batles client.
    - Both keyboard and gamepad inputs work!
	
## How To Use

- Change list of viewable custom battles
    - Keyboard: "Q" + "E"
    - Gamepad: Left Bumper + Right Bumper

## Installation

1. Download the latest BepInEx 5 version and install it into the folder with the Custom Battles client
    - BepInEx 5 downloads: https://github.com/BepInEx/BepInEx/releases
    - Installation Instructions: https://docs.bepinex.dev/articles/user_guide/installation/index.html
    - Default Client Location (Windows): `.../Steam/steamapps/common/Everhood/Everhood/EverhoodCustomBattles`
2. Download `EverhoodCB_BattleBrowser.dll` and place it into the `.../EverhoodCustomBattles/BepInEx/plugins` folder

## Planned Features

- [x] Add controller support
- [ ] Add mod configuration file
- [ ] Add UI elements to control browsing
- [ ] Add pagination, including...
    - [ ] Loading custom battle files on a per-page basis
    - [ ] Showing what page the user is currently on
- [ ] Add in-game search functionality
