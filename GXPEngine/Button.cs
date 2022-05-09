using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Button:AnimationSprite
{
    int target;
    public Button(int pTarget):base("square.png",1,1)
    {
        target = pTarget;
    }

    void Update()
    {
        if(HitTestPoint(Input.mouseX,Input.mouseY) && Input.GetMouseButtonDown(0))
        {
            ((MyGame)game).LoadLevel(target);
        }
    }
}