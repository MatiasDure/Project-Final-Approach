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

	public MyGame() : base(1920, 1080, false, true, 1280, 720)      // Create a window that's 800x600 and NOT fullscreen
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
		level.CreateLevel();
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

	//void AddPolygon(Vector2[] pPoints, int pRadius = 0, bool pCloseShape = true)
	//{
	//	if (pPoints.Length < 2) return;
	//	for (int i = pPoints.Length - 1; i > 0; i--) //going through array in reverse to avoid index out of bound exception
	//	{
	//		NLineSegment line = new NLineSegment(pPoints[i - 1], pPoints[i], 0xff00ff00, 4);
	//		AddChild(line);
	//		lines.Add(line);
	//	}
	//	if (pCloseShape)
	//	{
	//		NLineSegment closeLine = new NLineSegment(pPoints[pPoints.Length - 1], pPoints[0], 0xff00ff00, 4); //close opening by creating a line between first and last points
	//		AddChild(closeLine);
	//		lines.Add(closeLine);
	//	}
	//	AddLineCaps(pPoints, pRadius);
	//}

	//void AddLineCaps(Vector2[] pPoints, int pRadius = 0)
	//{
	//	for (int i = 0; i < pPoints.Length; i++)
	//	{
	//		Ball lineCap = new Ball(pRadius, pPoints[i], new Vector2(), new Vector2(), false);
	//		AddChild(lineCap);
	//		points.Add(lineCap);
	//	}
	//}

	//void AddLine(Vector2 pStart, Vector2 pEnd, int pRadius = 0, bool CloseShape = true) => AddPolygon(new Vector2[] { pStart, pEnd }, pRadius, CloseShape);

}
