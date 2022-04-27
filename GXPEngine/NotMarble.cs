using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class NotMarble:Ball
{

    public NotMarble(string pImgPath, int pCol, int pRow, TiledObject obj = null) : base(pImgPath, pCol, pRow)
    {
    }

    protected int detectionRange = 150;
    public Planet[] planets;

    public override void Step()
    {
        Pull(planets, detectionRange);
    }
}
