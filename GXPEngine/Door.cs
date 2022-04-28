using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Door:AnimationSprite
{
    Vector2 closePos;
    Vector2 openPos;
    int _id;
    public DoorButton connectedButton = null;
    bool open = true;

    public int Id { get => _id; }

    public Door(TiledObject obj = null):base("square.png", 1, 1)
    {
        if(obj != null)
        {
            _id = obj.GetIntProperty("id",-1);
            closePos = new Vector2(obj.X, obj.Y);
            openPos = new Vector2(obj.GetFloatProperty("openPosX", obj.X), obj.GetFloatProperty("openPosY", obj.Y - 200));          
        }
    }

    public void Step()
    {
        bool collision = connectedButton.CollisionWithPlanet();

        if (connectedButton != null && collision && !open)
        {
            open = true;
            Open();
        }
        else if (open && !collision)
        {
            open = false;
            Close();
        }
    }

    public void Open()
    {
        SetXY(openPos.x, openPos.y);
    }

    public void Close()
    {
        SetXY(closePos.x, closePos.y);
    }

}