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
    bool wasPlaying;
    bool wasPlaying2;
    bool colliding;
    SoundChannel beltSound; 

    public Vector2 Position { get => _position; }
    public Vector2 Movement { get => _movement; }
    public float Width { get => _width; }
    public float Height { get => _height; }

    public ConveyorBelt(TiledObject obj = null):base("conveyorBelt.png", 4, 1)
    {
        _movement = new Vector2();
        if(obj != null)
        {
            _movement = new Vector2(obj.GetFloatProperty("movementX", 0), obj.GetFloatProperty("movementY", 0)) * 2;
            _width = obj.Width;
            _height = obj.Height;
            _position.SetXY(obj.X+_width/2,obj.Y+_height/2);
            beltSound = new Sound("sounds/belt.wav", true, true).Play();
            beltSound.IsPaused = true;
        }
    }
    void FixPositions()
    {
        if (!fixedPoisitions)
        {
            SetOrigin(this.width / 2, this.height / 2);
            rotation = _movement.GetAngleDegrees() - 90;
            fixedPoisitions = true;
        }
    }

    void OnCollision(GameObject pOther)
    {
        colliding = false;
        if (pOther is Planet)
        {
            colliding = true;
            wasPlaying = true;
        }
    }

    public void Step()
    {
        try
        {
            if (wasPlaying) wasPlaying2 = true;
            else wasPlaying2 = false;
            if (wasPlaying2) beltSound.IsPaused = false;
            else beltSound.IsPaused = true;
            wasPlaying = false;

        }
        catch(Exception)
        {
            Console.WriteLine("You moron");
        }

        FixPositions();
        Animate(0.1f);
    }
}