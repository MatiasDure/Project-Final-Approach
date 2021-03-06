using System;									// System contains a lot of default C# libraries 
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine

// What needs to be done:
// -Fix collision check ---> Omair
// - New collision OnCollision() check for door to switch on and off collider ---> Omair
// - score system (highscore or stars? / based on timer or something else? (timer for now)) --->Matias
// - Implement json parser system to save highscore ---> Matias
// - Create buttons for hud ---> Matias
// - Alien ship laser beam new mechanism(three position checks, shrinking)---> Omair
// - Place buttons and hud elements on screen ---> Matias
// - Implement sound effects based on collisions---> Omair
// - Implement animation sprites and set cycles correctly---> Omair

public class MyGame : Game
{
	string levelName = null;
	int startingLevel = 0;
	int levelNum;
	public Planet planet;
	public Level level = null;
	Sound[] soundtrack;
	SoundChannel soundtrackChannel;
	Sprite background;

	public MyGame() : base(1920, 1080, false, false, 1280, 720) // Create a window that's 800x600 and NOT fullscreen
	{
		
		targetFps = 60;
		soundtrack = new Sound[] { new Sound("sounds/main_menu_music.mp3",true,true),
									new Sound("sounds/in_game_music.mp3",true,true) };
		OnAfterStep += CheckLevel;
		LoadLevel(startingLevel);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() 
	{
		if (Input.GetKeyDown(Key.TAB)) LoadLevel(levelNum);
	}

	static void Main()
	{
		Game myGame = new MyGame();
		myGame.Start();
	}

	void CheckLevel()
    {
		if (levelName == null) return;
		if (soundtrackChannel != null) soundtrackChannel.Stop();
		DestroyAll();
		background = new Sprite("background.png", false, false);
		AddChild(background);
		Sound soundToPlay;
		soundToPlay = levelNum < 2 ? soundtrack[0] : soundtrack[1];
		soundtrackChannel = soundToPlay.Play();
		level = new Level(levelName,levelNum);
		AddChild(level);
		level.CreateLevel();
		levelName = null;
    }

	public void LoadLevel(int plevel)
    {
		levelName = "level" + plevel + ".tmx";
		levelNum = plevel;
    }

	void DestroyAll()
    {
		List<GameObject> children = GetChildren();
		for(int i = 0; i < children.Count; i++)
        {
			children[i].Destroy();
        }
    }

}
