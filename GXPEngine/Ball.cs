using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public abstract class Ball:AnimationSprite
{    
    protected float _radius;
    protected Vector2 _position;
    public static float bounciness = 0.4f;

    public float Radius { get => _radius; }
    public Vector2 Position { get => _position; }

    public Ball(string pImgPath, int pCol, int pRow, TiledObject obj = null):base(pImgPath, pCol, pRow, -1, false, false)
    { 
    }

    virtual protected bool Init(TiledObject obj = null)
    {
        if (obj == null) return false;
        _position = new Vector2(obj.X + obj.Width / 2, obj.Y + obj.Height / 2);
        SetOrigin(this.width / 2, this.height / 2);
        _radius = obj.Width / 2;
        return true;
    }

    virtual protected void Initialize(TiledObject obj = null) 
    {
        if (!Init(obj)) return;
    }

    virtual public void Step() { }

}