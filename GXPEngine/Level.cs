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
    public Planet[] marbles;
    public List<NLineSegment> lines;
    public List<Caps> caps;
    Door[] doors;
    ConveyorBelt[] belts;
    Portal[] portals;
    Timer timer;
    Score score;
    ScoreInfo info;
    Hud hud;
    Pause pausedWindow;
    VelocityGizmos gravityIndicator;

    int currentLevel;
    int tiles = 17;
    int tileSize = 64;

    bool pause = false;
    bool addedPauseWindow = false;

    public Level(string pCurrentLevel, int pCurrentLevelNum)
    {
        SetXY(game.width/2 - tiles*tileSize/2, game.height/2 - tiles*tileSize / 2);
        loader = new TiledLoader(pCurrentLevel);
        Map levelData = MapParser.ReadMap(pCurrentLevel);
        currentLevel = pCurrentLevelNum;
        lines = new List<NLineSegment>();
        caps = new List<Caps>();
        info = new ScoreInfo();
        AddChild(info);
    }

    public void CreateLevel()
    {
        loader.addColliders = false;
        loader.rootObject = this;
        loader.LoadImageLayers();
        loader.LoadTileLayers();

        loader.autoInstance = true;
        loader.LoadObjectGroups();

        notMarbles = FindObjectsOfType<NotMarble>();
        marbles = FindObjectsOfType<Planet>();
        doors = FindObjectsOfType<Door>();
        belts = FindObjectsOfType<ConveyorBelt>();
        timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            score = new Score(timer);
            AddChild(score);
            hud = new Hud(currentLevel,score, info);
            parent.AddChild(hud);
        }

        ConnectingDoorToButton();
        ConnectingPortals();

        AddingElementsToPlanets();
        AddingPlanets();
        if (currentLevel > 1) CreateGravityIndicator();
    }

    void CreateGravityIndicator()
    {
        gravityIndicator = new VelocityGizmos();
        AddChild(gravityIndicator);
    }

    void Update()
    {
        if(currentLevel > 1)
        {
            if (!pause)
            {
                if (addedPauseWindow)
                {
                    pausedWindow.Destroy();
                    addedPauseWindow = false;
                }
                int win = 0;
                for (int i = 0; i < marbles.Length; i++)
                {
                    if (marbles[i].Win) win++;
                    marbles[i].Step();
                    if (marbles[i].Lost)
                    {
                        Sound loseSound = new Sound("sounds/you_lose.wav", false, true);
                        loseSound.Play();
                        LoadLvl(currentLevel);
                    }
                }
                if (win == marbles.Length)
                {
                    Sound winSound = new Sound("sounds/you_win.wav", false, true);
                    winSound.Play(); 
                    info.Save(currentLevel, score.Stars);

                    if (currentLevel == 8) LoadLvl(0);
                    else LoadLvl(++currentLevel);
                }

                if (hud != null)
                {
                    hud.Step();
                    pause = hud.Paused;
                }

                gravityIndicator.Step();
                foreach (Door door in doors) door.Step();
                foreach (NotMarble notMarble in notMarbles) notMarble.Step();
                foreach (ConveyorBelt belt in belts) belt.Step();

            }
            else
            {

                if (!addedPauseWindow)
                {
                    pausedWindow = new Pause(currentLevel);
                    parent.AddChild(pausedWindow);
                    addedPauseWindow = true;
                }
                pausedWindow.Step();
                pause = pausedWindow.Paused;
            }
        }
    }

    void AddingPlanets()
    {
        for (int i = 0; i < notMarbles.Length; i++)
        {
            notMarbles[i].planets = marbles;
        }

        for(int i = 0; i < belts.Length; i++)
        {
            belts[i].planets = marbles;
        }
    }

    void AddingElementsToPlanets()
    {
        for(int i = 0; i < marbles.Length; i++)
        {
            marbles[i].notMarbles = notMarbles;
            marbles[i].belts = belts;
        }
    }

    void ConnectingPortals()
    {
        for (int i = 0; i < notMarbles.Length; i++)
        {
            if(notMarbles[i] is Portal portal && portal.connectedPortal == null)
            {
                bool foundPortal = false;
                foreach (NotMarble notMarble in notMarbles)
                {
                    if (notMarble is Portal otherPortal && 
                        otherPortal != portal && 
                        otherPortal.Id == portal.Id &&
                        otherPortal.connectedPortal == null)
                    {
                        portal.connectedPortal = otherPortal;
                        otherPortal.connectedPortal = portal;
                        foundPortal = true;
                    }
                    if (foundPortal) break;
                }
            }
        }
    }

    void LoadLvl(int pLevel)
    {
        ((MyGame)game).LoadLevel(pLevel);
    }

    void ConnectingDoorToButton()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            bool foundButton = false;
            foreach(NotMarble notMarble in notMarbles)
            {
                if(notMarble is DoorButton dB && doors[i].Id == dB.Id)
                {
                    doors[i].connectedButton = dB;
                    foundButton = true;
                }
                if (foundButton) break;
            }
        }
    }

    public int BallCount() => marbles.Length;
    public int CapsCount() => caps.Count;
    public int LineCount() => lines.Count;
    public Ball BallAtIndex(int pIndex) => marbles[pIndex];
    public Caps CapsAtIndex(int pIndex) => caps[pIndex];
    public NLineSegment LineAtIndex(int pIndex) => lines[pIndex];

}
