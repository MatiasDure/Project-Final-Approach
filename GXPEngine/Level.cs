using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Level: GameObject
{
    TiledLoader loader;
    Blackhole[] balls;
    Planet[] marbles;
    NLineSegment[] lines;
    string currentLevel;

    public Level(string pCurrentLevel)
    {
        loader = new TiledLoader(pCurrentLevel);
        Map levelData = MapParser.ReadMap(pCurrentLevel);
        currentLevel = pCurrentLevel;
        marbles = new Planet[2];
        CreateLevel();
    }

    public void CreateLevel()
    {
        loader.LoadImageLayers();
        loader.LoadTileLayers();

        loader.autoInstance = true;
        loader.LoadObjectGroups();

        balls = FindObjectsOfType<Blackhole>();
        marbles = FindObjectsOfType<Planet>();

        for(int i = 0; i < balls.Length; i++)
        {
            balls[i].planets = marbles;
        }

        lines = FindObjectsOfType<NLineSegment>();
    }

    void Update()
    {
        Console.WriteLine(marbles.Length);
        foreach (Planet p in marbles) p.Step();
        foreach(Blackhole b in balls) b.Step();

    }

    public int BallCount() => balls.Length;
    public int LineCount() => lines.Length;
    public Ball BallAtIndex(int pIndex) => balls[pIndex];
    public NLineSegment LineAtIndex(int pIndex) => lines[pIndex];
}