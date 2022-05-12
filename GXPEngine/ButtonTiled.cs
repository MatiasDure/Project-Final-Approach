using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class ButtonTiled : AnimationSprite
{
    int target;
    int _id;

    public ButtonTiled(TiledObject obj = null) : base("buttons/levels.png", 2, 1)
    {
        if (obj != null)
        {
            target = obj.GetIntProperty("target", 0);
            _id = obj.GetIntProperty("id", 1);
        }
        base._texture = new AnimationSprite("buttons/level"+(target-1)+".png", 2, 1).texture;
    }

    public void Update()
    {
        bool clickedButton = Clicked();
        if (clickedButton) LoadLvl();
    }

    void LoadLvl()
    {
        ((MyGame)game).LoadLevel(target);
    }

    bool Clicked()
    {
        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            currentFrame = 1;
            if (Input.GetMouseButtonDown(0)) return true;
        }
        else currentFrame = 0;
        return false;
    }

}