using System;									// System contains a lot of default C# libraries 
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
	public Level currentLevel;
	string startingLevel;
	public Planet planet;
	Blackhole black;

	public MyGame() : base(1280, 720, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		startingLevel = "levelOne";
		planet = new Planet(new Vector2(100,100));
		black = new Blackhole(new Vector2(700,300));
		AddChild(planet);
		AddChild(black);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() 
	{
		planet.Step();
        Console.WriteLine(planet.acceleration);
	}

	static void Main()
	{
		Game myGame = new MyGame();
		myGame.Start();
	}

}
