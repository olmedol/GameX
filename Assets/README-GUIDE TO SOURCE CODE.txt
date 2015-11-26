-Assets Folder	
	All our source code is located within the Assets folder.
	-Audio Folder contains all audio files we used to make this game
		explosions, music, etc.
	-Editor Folder, Photon Unitiy Networking Folder, and Plugins Folder
		Plugins we downloaded that are required to use the PUN service
		These can mostly be ignored as we didn't write these, and only used 
		some of the files in order to get multiplayer working.
	-Prefabs
		Items in here are all .prefab files. They behave as templates for in game objects.
		They define the object, you need Unity editor in order to be able to see these in detail.
	-Scenes
		This folder contains two files, scene files require the unity editor to open
		They both define the look and behavior of the games only two scenes, the main menu, and the
		game world where you actually play the game. 
	-Scripts Folder
		The meat of what we wrote, this is mostly where all the code we wrote is contained.
		They describe certain behaviors for certain objects, they are mostly attached to prefabs as components
		which is how you give objects unique behaviors. 

		-Multiplayer Folder
			Contains all the code we had to write to implement multiplayer.
	-Standard assets
		Basic Assets we required
		-2D Folder
			-Scripts Folder
				Holds our more important scripts, these are for defining enemy behaviors
				defining level up systems, player controls and behaviors, world behaviors.
			
			
	
	