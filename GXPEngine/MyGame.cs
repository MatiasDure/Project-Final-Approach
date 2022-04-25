using System;									// System contains a lot of default C# libraries 
using System.Collections.Generic;
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
	
	public MyGame() : base(1280, 720, false)		// Create a window that's 800x600 and NOT fullscreen
	{
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() //Test
	{

	}

	static void Main()
	{
		Game myGame = new MyGame();
		myGame.Start();
	}

}
