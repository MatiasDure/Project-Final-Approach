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


    public Vector2 Position { get => _position; }
    public Vector2 Movement { get => _movement; }
    public float Width { get => _width; }
    public float Height { get => _height; }

    public ConveyorBelt(TiledObject obj = null):base("square.png", 1, 1)
    {
        _movement = new Vector2();
        if(obj != null)
        {
            _position.SetXY(obj.X,obj.Y);
            _movement = new Vector2(obj.GetFloatProperty("movementX", 0), obj.GetFloatProperty("movementY", 0));
            SetOrigin(this.width / 2, this.height / 2);
            _width = obj.Width;
            _height = obj.Height;
        }
    }

    public void Step()
    {
        SetOrigin(this.width / 2, this.height / 2);
        //CollisionWithPlanet();
    }

    //void CollisionWithPlanet()
    //{
    //    foreach (Planet p in planets)
    //    {
    //        Vector2 difference = _position - p.Position;
    //        float distance = difference.Length();

    //        if (distance < 105)
    //        {
    //            EnableMovement(p);
    //        }
    //        else if(distance > 120) p.riding = false;
    //    }
    //}

    //void EnableMovement(Planet pP)
    //{
    //    pP.RidingConveyorBelt(_movement);
    //}

}