Instructions  
		
	Currently runs starting from any scene, but it is supposed to be started at scene Menu.unity;
 	Works with either of the following Scenes enabled on BuildSettings:
		- Scenes/Menu; Scenes/Main;
		- Scenes/Menu; Scenes/Main Alternative;
	
 Observations
 	
	Main Alternative = Main without "LOAD" and "NEW GAME" buttons, they are interchangeable as stated above;
 
	Menu Scene
		When selecting "NEW GAME":
			- A new panel to select the game card layout will be shown values are set to be between 2 and 6, code supports any layout value but only 2x2 to 6x6 was tested with visuals;
			- "START GAME" button will only be interactable if a valid layout is selected, a valid layout is a layout with at least 4 cards and with a pair number of total cards;
  
