using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class Score : GameObject
{
    int _stars = 3;
    Timer timer;
    float fraction1;
    float fraction2;
    float fraction3;

    public int Stars { get => _stars; }

    public Score(Timer pTimer)
    {
        timer = pTimer;
        fraction1 = timer.LevelTime * (3f / 4f);
        fraction2 = timer.LevelTime * (2f / 4f);
        fraction3 = timer.LevelTime * (1f / 4f);

        
    }

    void Update()
    {
        UpdateStars();
    }

    void UpdateStars()
    {
        if (timer.TimeLeft <= timer.LevelTime && timer.TimeLeft >= fraction1) _stars = 3;
        else if (timer.TimeLeft < fraction1 && timer.TimeLeft >= fraction2) _stars = 2;
        else if (timer.TimeLeft < fraction2 && timer.TimeLeft >= fraction3) _stars = 1;
        else if (timer.TimeLeft < fraction3) _stars = 0;
    }

}