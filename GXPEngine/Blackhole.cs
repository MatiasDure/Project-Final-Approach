using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Blackhole:NotMarble
{

    public Blackhole( TiledObject obj = null) :base("blackhole.png", 1, 1)
    {
        Init(obj);
    }
}
