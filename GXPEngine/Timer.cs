﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Timer:Sprite
{
    int _timer;
    public int TimeLeft { get => _timer; }
    
    public Timer(TiledObject obj):base("empty.png", false)
    {
        Console.WriteLine("Timer");
        if(obj != null)
        {
            _timer = obj.GetIntProperty("timer");
        }
    }

    void Update()
    {
        UpdateTimer();
    }

    void DisplayTimer()
    {

    }

    void UpdateTimer()
    {
        Console.WriteLine(Time.time);
        if (!(TimeLeft > 0)) return;
        _timer -= Time.deltaTime;
        
    }
}