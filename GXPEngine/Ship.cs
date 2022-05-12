using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Ship:NotMarble
{
    public bool used = false; 
    public Ship(TiledObject obj = null) :base("ufoIdleSpritesheet.png", 4, 5)
    {
        Init(obj);
        detectionRange = 50;
        SetCycle(0, 4);
    }

    public override void Step()
    {
        Animate(0.1f);
    }
}
