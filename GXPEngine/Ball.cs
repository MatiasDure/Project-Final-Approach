using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public abstract class Ball:AnimationSprite
{    
    int _radius;
    protected Vector2 _position;
    public static float bounciness = 1f;

    public int Radius { get => _radius; }
    public Vector2 Position { get => _position; }

    public Ball(string pImgPath, int pCol, int pRow, Vector2 pPos):base(pImgPath, pCol, pRow, -1, false, false)
    {

    }
}