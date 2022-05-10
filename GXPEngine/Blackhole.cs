using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Blackhole:NotMarble
{
    bool movable = false;
    bool pull = false;
    Vector2 Velocity;
    float distanceToMove;
    Vector2 startingPosition;
    Vector2 currentPosition;

    public Blackhole( TiledObject obj = null) :base("blackhole.png", 1, 1)
    {
        Initialize(obj);
    }

    protected override void Initialize(TiledObject obj = null)
    {
        if (!base.Init(obj)) return;
        currentPosition = startingPosition = new Vector2(obj.X, obj.Y);      
        movable = obj.GetBoolProperty("movable",false);
        Console.WriteLine(movable);
        if (movable)
        {
            Velocity = new Vector2(obj.GetFloatProperty("velocityX", 0), obj.GetFloatProperty("velocityY", 0));
            distanceToMove = obj.GetFloatProperty("distance",0);
        }
    }

    public override void Step()
    {
        if(movable)
        {
            if (!pull)
            {
                Move();
                UpdateScreenPosition();
            }
            MovingPull();
        }
        else base.Step();
    }

    void Move()
    {
        currentPosition += Velocity;
        float distance = (startingPosition - currentPosition).Length();
        if (distance > distanceToMove ||
           (currentPosition.x == startingPosition.x &&
           currentPosition.y == startingPosition.y)) TurnAround();
        //if (distance > distanceToMove) TurnAround();
    }

    void TurnAround()
    {
        Velocity.SetXY(-Velocity.x,-Velocity.y);
    }

    void UpdateScreenPosition()
    {
        x = currentPosition.x;
        y = currentPosition.y;
    }

    void MovingPull()
    {
        foreach (Planet p in planets)
        {
            Vector2 difference = currentPosition - p.Position;
            float distance = difference.Length();

            if (distance < detectionRange)
            {
                pull = true;
                p.SuckedIn(difference, this);
            }
        }
    }

}
