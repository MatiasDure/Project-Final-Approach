using System;									// System contains a lot of default C# libraries 
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine

// TO DO:
// add hud
// add buttons
// fix collision check
// Highscore
// add sounds
// Change door mechanism to dissapear 

public class MyGame : Game
{
	string levelName = null;
	int startingLevel = 2;
	int levelNum;
	public Planet planet;
	public Level level = null;
	Sound[] soundtrack;
	SoundChannel soundtrackChannel;

	public MyGame() : base(1920, 1080, false, true, 1280, 720)      // Create a window that's 800x600 and NOT fullscreen
	{
		soundtrack = new Sound[] { new Sound("sounds/mainMenuSoundtrack.mp3",true,true),
									new Sound("sounds/gameplaySoundtrack.mp3",true,true) };
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
		Sound soundToPlay;
		soundToPlay = levelNum == 0 ? soundtrack[0] : soundtrack[1];
		//soundtrackChannel = soundToPlay.Play();
		level = new Level(levelName,levelNum);
		level.CreateLevel();
		AddChild(level);
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
