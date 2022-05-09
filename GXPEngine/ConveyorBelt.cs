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
    Vector2 position;
    Vector2 movement;

    public ConveyorBelt(TiledObject obj = null):base("square.png", 1, 1)
    {
        movement = new Vector2();
        if(obj != null)
        {
            position.SetXY(obj.X,obj.Y);
            movement = new Vector2(obj.GetFloatProperty("movementX", 0), obj.GetFloatProperty("movementY", 0));
            SetOrigin(this.width / 2, this.height / 2);
        }
    }

    public void Step()
    {
        SetOrigin(this.width / 2, this.height / 2);
        CollisionWithPlanet();
    }

    void CollisionWithPlanet()
    {
        foreach (Planet p in planets)
        {
            Vector2 difference = position - p.Position;
            float distance = difference.Length();

            if (distance < 105)
            {
                EnableMovement(p);
            }
            else if(distance > 120) p.riding = false;
        }
    }

    void EnableMovement(Planet pP)
    {
        pP.RidingConveyorBelt(movement);
    }

}