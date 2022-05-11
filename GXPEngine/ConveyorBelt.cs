using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class ConveyorBelt:AnimationSprite
{
    public Planet[] planets;
    Vector2 _position;
    Vector2 _movement;
    float _width, _height;
    bool fixedPoisitions;


    public Vector2 Position { get => _position; }
    public Vector2 Movement { get => _movement; }
    public float Width { get => _width; }
    public float Height { get => _height; }

    public ConveyorBelt(TiledObject obj = null):base("conveyorBelt.png", 4, 1)
    {
        _movement = new Vector2();
        if(obj != null)
        {
            _position.SetXY(obj.X,obj.Y);
            _movement = new Vector2(obj.GetFloatProperty("movementX", 0), obj.GetFloatProperty("movementY", 0));
            //SetOrigin(this.width / 2, this.height / 2);
            _width = obj.Width;
            _height = obj.Height;
           // rotation = _movement.GetAngleDegrees();
        }
    }
    void FixPositions()
    {
        if (!fixedPoisitions)
        {
            SetOrigin(this.width / 2, this.height / 2);
            rotation = _movement.GetAngleDegrees() - 90;
            if(rotation == -90) y -= this.height;
            if(rotation == 90) x -= this.width;
            if(rotation == -180)
            {
                x -= this.width;
                y -= this.height;
            }
        }

    }
    public void Step()
    {
        FixPositions();
        fixedPoisitions = true;
        Animate(0.05f);
    }

}