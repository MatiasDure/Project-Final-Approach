using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public abstract class Ball:AnimationSprite
{    
    int _radius;
    protected Vector2 _position;
    public static float bounciness = 1f;

    public int Radius { get => _radius; }
    public Vector2 Position { get => _position; }

    public Ball(string pImgPath, int pCol, int pRow, TiledObject obj = null):base(pImgPath, pCol, pRow, -1, false, false)
    {
    }

    virtual protected void Init(TiledObject obj = null)
    {
        if(obj != null)
        {
            _position = new Vector2(obj.X + obj.Width / 2, obj.Y + obj.Height / 2);
        }
    }

    virtual public void Step() { }

    protected void Pull(Planet[] pPlanets, int pDetectionRange)
    {
        foreach (Planet p in pPlanets)
        {
            Vector2 difference = _position - p.Position;
            float distance = difference.Length();

            if (distance < pDetectionRange) p.SuckedIn(difference, this);
        }
    }
}