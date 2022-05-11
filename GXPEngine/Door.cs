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
    float delay = 1;
    List<String> images;

    public int Id { get => _id; }

    public Door(TiledObject obj = null):base("rock1.png", 3, 1)
    {
        if(obj != null)
        {
            images = new List<String>();
            images.Add("rock1.png");
            images.Add("rock2.png");
            images.Add("rock3.png");
            images.Add("rock4.png");
            //base.

            _id = obj.GetIntProperty("id",-1);
            base._texture = new Sprite(images[(_id - 1) % 4]).texture;
            closePos = new Vector2(obj.X, obj.Y);
            openPos = new Vector2(obj.GetFloatProperty("openPosX", obj.X), obj.GetFloatProperty("openPosY", obj.Y - 200));
        }
    }

    public void Step()
    {
        bool collision = connectedButton.CollisionWithPlanet();
        if (collision)
        {
            if (connectedButton.currentFrame < 2) connectedButton.Animate(0.2f);
            delay = 1; 
        }
        else
        {
            connectedButton.currentFrame = 0;
            if (delay < 0.5) currentFrame = 1; 
            if (delay < 0) currentFrame = 0;
            delay -= 0.05f;
        } 

            if (disableCollider && currentFrame != 2) Animate(0.1f);
        //if(!disableCollider) currentFrame = 0;
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
