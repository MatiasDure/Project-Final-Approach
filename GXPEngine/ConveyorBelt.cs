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

    public ConveyorBelt(TiledObject obj = null):base("conveyorBelt.png", 1, 1)
    {
        _movement = new Vector2();
        if(obj != null)
        {
            _movement = new Vector2(obj.GetFloatProperty("movementX", 0), obj.GetFloatProperty("movementY", 0));
            _width = obj.Width;
            _height = obj.Height;
            _position.SetXY(obj.X + width/2,obj.Y + width/2);
            //SetOrigin(this._width / 2, this._height / 2);
            //Console.WriteLine(_width);
            UpdateScreenPos();
        }
    }

    void UpdateScreenPos()
    {
        x = _position.x;
        y = _position.y;
    }

    public void Step()
    {
        //SetOrigin(this.Width / 2, this.Height / 2);
    }

}