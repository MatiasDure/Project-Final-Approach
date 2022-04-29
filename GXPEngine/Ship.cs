using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Ship:NotMarble
{

    public Ship(TiledObject obj = null) :base("ship.png", 1, 1)
    {
        Init(obj);
        detectionRange = 50;
    }

}
