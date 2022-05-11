﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Score:GameObject
{
    int _stars = 3;
   
    Timer timer;


    public int Stars { get => _stars; }

    public Score(Timer pTimer)
    {
        timer = pTimer;    
    }

    void Update()
    {
        UpdateStars();
        //Console.WriteLine(timer.TimeLeft);
    }

    void UpdateStars()
    {
        float fraction1 = timer.LevelTime * (3 / 4);
        float fraction2 = timer.LevelTime * (2 / 4);
        float fraction3 = timer.LevelTime * (1 / 4);

        if (timer.TimeLeft <= timer.LevelTime && timer.TimeLeft >= fraction1)
        {
            _stars = 3;
            //Console.WriteLine("hey");
        }
        else if (timer.TimeLeft < fraction1 && timer.TimeLeft >= fraction2) _stars = 2;
        else if (timer.TimeLeft < fraction2 && timer.TimeLeft >= fraction3) _stars = 1;
        else if(timer.TimeLeft < fraction3) _stars = 0;
    }

}