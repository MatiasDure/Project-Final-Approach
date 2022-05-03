using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Caps : Ball
{

   
    public Caps(Vector2 pPos , float pRadius) : base("empty.png", 1,1)
    {
        SetOrigin(width / 2, height / 2);
        //Clear(255);
        _position = pPos;
        _radius = pRadius;

        x = _position.x;
        y = _position.y;
    }
}
