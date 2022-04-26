using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public abstract class Ball:AnimationSprite
{
    public Ball(string pImgPath, int pCol, int pRow):base(pImgPath, pCol, pRow, -1, false, false)
    {

    }

}