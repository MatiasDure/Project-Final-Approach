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
    NotMarble[] notMarbles;
    Planet[] marbles;
    NLineSegment[] lines;
    int currentLevel;

    public Level(string pCurrentLevel, int pCurrentLevelNum)
    {
        loader = new TiledLoader(pCurrentLevel);
        Map levelData = MapParser.ReadMap(pCurrentLevel);
        currentLevel = pCurrentLevelNum;
    }

    public void CreateLevel()
    {
        loader.addColliders = false;
        loader.LoadImageLayers();

        loader.rootObject = this;

        loader.autoInstance = true;
        loader.LoadObjectGroups();

        notMarbles = FindObjectsOfType<NotMarble>();
        marbles = FindObjectsOfType<Planet>();

        
        AddingPlanets();

        lines = FindObjectsOfType<NLineSegment>();
    }

    void Update()
    {
        for (int i = 0; i < marbles.Length ;i++)
        {
            marbles[i].Step();
            if(i == 0 && marbles[i].Win && marbles[i+1].Win)
            {
                ((MyGame)game).LoadLevel(++currentLevel);
            }
            if (marbles[i].Lost) ((MyGame)game).LoadLevel(currentLevel);
        }

        foreach (NotMarble notMarble in notMarbles) notMarble.Step();
    }

    void AddingPlanets()
    {
        for (int i = 0; i < notMarbles.Length; i++)
        {
            notMarbles[i].planets = marbles;
        }
    }

    public int BallCount() => notMarbles.Length;
    public int LineCount() => lines.Length;
    public Ball BallAtIndex(int pIndex) => notMarbles[pIndex];
    public NLineSegment LineAtIndex(int pIndex) => lines[pIndex];

}