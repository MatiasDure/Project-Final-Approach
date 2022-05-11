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
        //Console.WriteLine(level1Score);
    }

    public void Save(int pLevel, int pScore)
    {
        try
        {
            //attributes that change throughout game
            if (levelScores[pLevel - 1] >= pScore) return;
            scores.GetValue("level1").Replace(pScore);
            File.WriteAllText(jsonPath, scores.ToString());
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
        }
        catch (Exception) { Console.WriteLine("Error while loading! Game was not loaded properly."); }
    }
}