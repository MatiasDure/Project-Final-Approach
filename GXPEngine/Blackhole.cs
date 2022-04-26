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

    public Blackhole( TiledObject obj = null) :base("circle.png", 1, 1)
    {
        _position = new Vector2(obj.X, obj.Y);
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
                p.pull = true;
                p.acceleration = difference.Normalized() * p.velocity.Length() * 0.05f;
                p.desVelocity = difference.Normalized();
            }
        }    
        
    }
}
