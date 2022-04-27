using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Blackhole:Ball
{
    int detectionRange = 100;
    public Planet[] planets;

    public Blackhole( TiledObject obj = null) :base("blackhole.png", 1, 1)
    {
        _position = new Vector2(obj.X + obj.Width/2, obj.Y + obj.Height/2);
    }

    public override void Step()
    {
        TryingOutPull();
    }

    void TryingOutPull()
    {
        //Planet p = ((MyGame)game).planet;
        //if (p == null) return;
        foreach(Planet p in planets)
        {
            Vector2 difference = _position - p.Position;
            float distance = difference.Length();

            if (distance < detectionRange)
            {
                p.SuckedIn(difference);
            }
        }    
        
    }
}
