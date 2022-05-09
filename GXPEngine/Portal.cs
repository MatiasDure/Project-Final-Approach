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
            //Console.WriteLine(distance);

            if (distance < 80)
            {
                if (!p.teleporting)
                {
                    connectedPortal.teleportedTo = true;
                    p.Teleport(connectedPortal.Position);
                    p.teleporting = true;
                }
            }
            else if (p.teleporting && teleportedTo && distance > 130)
            {
                p.teleporting = false;
                teleportedTo = false;
            }
        }
    }

}

