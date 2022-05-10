using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public abstract class NotMarble:Ball
{
    protected int detectionRange = 150;
    public Planet[] planets;
    protected float _width, _height;

    public float Width { get => _width; }
    public float Height { get => _height; }
    
    public NotMarble(string pImgPath, int pCol, int pRow, TiledObject obj = null) : base(pImgPath, pCol, pRow)
    {
    }

    public override void Step()
    {
        Pull();
    }

    void Pull()
    {
        foreach (Planet p in planets)
        {
            Vector2 difference = _position - p.Position;
            float distance = difference.Length();

            if (distance < detectionRange) p.SuckedIn(difference, this);
        }
    }

    public bool CollisionWithPlanet()
    {
        foreach (Planet p in planets)
        {
            Vector2 difference = _position - p.Position;
            float distance = difference.Length();
            float distanceForCenter = p.Width/2 + this.Width/2;

            if (distance < distanceForCenter) return true;
        }
        return false;
    }


}
