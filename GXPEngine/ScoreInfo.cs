using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using GXPEngine;

public class ScoreInfo : GameObject
{
    const string jsonPath = @"scoreSave.json";
    JObject scores;
    public bool gameSaved;
    int[] levelScores;
    int level1Score;
    int level2Score;
    int level3Score;
    int level4Score;
    int level5Score;
    int level6Score;
    int level7Score;
    int level8Score;

    public ScoreInfo()
    {
        levelScores = new int[8];
        try
        {
            scores = JObject.Parse(File.ReadAllText(jsonPath));
        }
        catch (Exception) { Console.WriteLine("Issue while trying to read JSON file!!"); }

        Load();
    }

    void Update()
    {
        Console.WriteLine(level1Score);
    }

    public void Save(int pLevel)
    {
        try
        {
            //attributes that change throughout game    
            
            Console.WriteLine("Game Saved!");
            gameSaved = false;
        }
        catch (Exception) { Console.WriteLine("Error while saving! Game was not saved properly."); }
    }

    public void Load()
    {
        try
        {
            for(int i = 0; i < levelScores.Length; i++)
            {
                levelScores[i] = (int)scores["level" + (i+1)];
            }
            //level1Score = (int)scores["level1"];
            //level2Score = (int)scores["level2"];
            //level3Score = (int)scores["level3"];
            //level4Score = (int)scores["level4"];
            //level5Score = (int)scores["level5"];
            //level6Score = (int)scores["level6"];
            //level7Score = (int)scores["level7"];
            //level8Score = (int)scores["level8"];
        }
        catch (Exception) { Console.WriteLine("Error while loading! Game was not loaded properly."); }
    }
}