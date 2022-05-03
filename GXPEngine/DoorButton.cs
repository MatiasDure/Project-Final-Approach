using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class DoorButton:NotMarble
{
    int _id;

    public int Id { get => _id; } 

    public DoorButton(TiledObject obj = null):base("buttonDoor.png",1,1)
    {
        Initialize(obj);
    }

    protected override void Initialize(TiledObject obj = null)
    {
        if(!base.Init(obj)) return;
        _id = obj.GetIntProperty("id", -1);
    }

    public override void Step()
    {
    }

}