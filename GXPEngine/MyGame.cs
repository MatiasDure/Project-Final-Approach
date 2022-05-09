using System;									// System contains a lot of default C# libraries 
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine

// TO DO:
// Button and door mechanic
// Portal mechanic
// Conveyor belt mechanic

public class MyGame : Game
{
	string levelName = null;
	int startingLevel = 6;
	int levelNum;
	public Planet planet;
	public Level level = null;

	public MyGame() : base(1920, 1080, false, true, 1280, 720)      // Create a window that's 800x600 and NOT fullscreen
	{
		OnAfterStep += CheckLevel;
		LoadLevel(startingLevel);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() 
	{
	}

	static void Main()
	{
		Game myGame = new MyGame();
		myGame.Start();
	}

	void CheckLevel()
    {
		if (levelName == null) return;
		DestroyAll();
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
