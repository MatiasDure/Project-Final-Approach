using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Portal:NotMarble
{
    public Portal connectedPortal = null;
    int _id;

    public bool teleportedTo = false;

    public int Id { get => _id; }

    public Portal(TiledObject obj=null):base("portal.png",1,1)
    {
        Initialize(obj);
    }

    protected override void Initialize(TiledObject obj = null)
    {
        if(base.Init(obj)) return;
        _id = obj.GetIntProperty("id", -1);
    }

    public override void Step()
    {
        CollisionWPlanet();
    }

    void CollisionWPlanet()
    {
        foreach (Planet p in planets)
        {
            Vector2 difference = _position - p.Position;
            float distance = difference.Length();
            float differenceFromCenter = Mathf.Abs(this.Width / 2 + p.Width / 2);
            //Console.WriteLine(distance);

            if (distance < differenceFromCenter)
            {
                if (!p.teleporting)
                {
                    connectedPortal.teleportedTo = true;
                    p.Teleport(connectedPortal.Position);
                    p.teleporting = true;
                }
            }
            else if (p.teleporting && teleportedTo && distance > differenceFromCenter + 30)
            {
                p.teleporting = false;
                teleportedTo = false;
            }
        }
    }

}

