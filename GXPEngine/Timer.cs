using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

//public class Timer:Sprite
//{
//    int _timer;
//    int _levelTime;
//    public int TimeLeft { get => _timer; }
    
//    public Timer(TiledObject obj):base("empty.png", false)
//    {
//        //Console.WriteLine("Timer");
//        if(obj != null)
//        {
//            _timer = obj.GetIntProperty("timer");
//        }
//    }

//    void Update()
//    {
//        UpdateTimer();
//    }

//    void DisplayTimer()
//    {

//    }

//    void UpdateTimer()
//    {
//        int t = Time.time % 1000;
//        //Console.WriteLine(t);
//        if (!(TimeLeft > 0)) return;
        
//    }
//}

//using GXPEngine;
//using TiledMapParser;

//public class Timer : Sprite
public class Timer : Sprite
{
    float _timer;
    float _levelTime;
    int newSecond;
    int oldSecond;
    public float TimeLeft { get => _timer; }
    public float LevelTime { get => _levelTime; }

    public Timer(TiledObject obj) : base("empty.png", false)
    {
        Console.WriteLine("Timer");
        //Console.WriteLine("Timer");
        if (obj != null)
        {
            _timer = obj.GetIntProperty("timer");
            _levelTime = _timer;
        }
    }

    void Update()
    {
        oldSecond = newSecond;
        newSecond = Time.time / 1000;
        if (newSecond != oldSecond) UpdateTimer();
    }

    void DisplayTimer()
    {

    }

    void UpdateTimer()
    {
        if (TimeLeft <= 0) return;
        _timer--;
        //Console.WriteLine(TimeLeft);
    }
}