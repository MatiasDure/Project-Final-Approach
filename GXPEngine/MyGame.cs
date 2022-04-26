using System;									// System contains a lot of default C# libraries 
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
	string levelName = null;
	int startingLevel = 1;
	public Planet planet;
	Blackhole black;
	public Level level = null;

	public MyGame() : base(1920, 1080, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		//planet = new Planet(new Vector2(100,100));
		//black = new Blackhole(new Vector2(700,300));
		//AddChild(planet);
		//AddChild(black);
		OnAfterStep += CheckLevel;
		LoadLevel(startingLevel);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() 
	{
		//planet.Step();
  //      Console.WriteLine(planet.acceleration);
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
		level = new Level(levelName);
		AddChild(level);
		levelName = null;
    }

	void LoadLevel(int plevel)
    {
		levelName = "level" + plevel + ".tmx";
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
