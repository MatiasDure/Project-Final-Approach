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
    public bool disableCollider = false; 

    public int Id { get => _id; }

    public Door(TiledObject obj = null):base("rockCyanSprite.png", 3, 1)
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

        if(disableCollider && currentFrame != 2) Animate(0.05f);
        if(!disableCollider) currentFrame = 0;
        if (connectedButton != null && collision) disableCollider = true;//OpenClose(openPos);
        else if (!collision)
        {
            if (open)
            {
                SetXY(closePos.x, closePos.y);
                open = false;
            }
            disableCollider = false;
            // OpenClose(closePos);
        }
    }

    public void OpenClose(Vector2 pPos)
    {
        if (y < pPos.y) y++;
        else if (y > pPos.y) y--;
    }

}
