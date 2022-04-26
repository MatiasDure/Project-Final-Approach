using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Blackhole:Ball
{
    //Vector2 _position; 
    public Blackhole(Vector2 pPos) :base("circle.png", 1, 1, pPos)
    {
        _position = pPos;
        x = _position.x;
        y = _position.y;
    }

    void Update()
    {
        TryingOutPull();
    }

    void TryingOutPull()
    {
        Planet p = ((MyGame)game).planet;
        Vector2 difference = _position - p.Position ;
        float distance = difference.Length();
        int detectionRange = 100;

        if (distance < detectionRange)
        {
            p.pull = true;
            p.acceleration = difference.Normalized() * p.velocity.Length() * 0.05f; 
            p.desVelocity = difference.Normalized();
        }
    }
}
