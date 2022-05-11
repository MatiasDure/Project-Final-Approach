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
    List<String> images;

    public DoorButton(TiledObject obj = null):base("doorButton1.png", 3,1)
    {


        Initialize(obj);
        images = new List<String>();
        images.Add("doorButton1.png");
        images.Add("doorButton2.png");
        images.Add("doorButton3.png");
        images.Add("doorButton4.png");
        _id = obj.GetIntProperty("id", -1);
        base._texture = new Sprite(images[(_id - 1)%4]).texture;
    }

    protected override void Initialize(TiledObject obj = null)
    {
        if(!base.Init(obj)) return;
        _id = obj.GetIntProperty("id", -1);
        _width = obj.Width;
        _height = obj.Height;
    }

    public override void Step()
    {
    }

}