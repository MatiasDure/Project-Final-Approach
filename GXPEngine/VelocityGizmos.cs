using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine; 

public class VelocityGizmos: EasyDraw
{
    List<EasyDraw> shaft;
    EasyDraw tip;
    EasyDraw gizmosBase; 
    Vector2 velocity;
    int count;
    int oldCount = 0;
    float magnituge;
    //SoundChannel boardMoving; 

    public VelocityGizmos() : base(100, 100)
    {
        shaft = new List<EasyDraw>();
        tip = new EasyDraw("gizmosTip.png", false);
        gizmosBase = new EasyDraw("gizmosBase.png", false);
        tip.SetOrigin(tip.width / 2, tip.height / 2);
        gizmosBase.SetOrigin(gizmosBase.width/2, gizmosBase.height/2);
        AddChild(gizmosBase);
        SetOrigin(50, 50);
        //boardMoving = new Sound("sounds/board_moving.wav",true,true).Play();
        x = 100;
        y = 100;        
    }

    void addToShaft(int value)
    {
        EasyDraw newShaft = new EasyDraw("gizmosShaft.png", false);
        newShaft.SetOrigin(newShaft.width / 2, newShaft.height / 2);   
        AddChild(newShaft);
        shaft.Add(newShaft);
        AddChild(tip);
        AddChild(gizmosBase);
    }
    void UpdatePositons()
    {
        tip.x = magnituge + tip.width/2;
        for (int i = 0; i < shaft.Count; i++)
        {
           shaft.ElementAt(i).x = magnituge - (i * 15) - (shaft.ElementAt(i).width/2);
        }
    }
    void Direction()
    {
        rotation = velocity.GetAngleDegrees();
    }
    void Magnitude()
    {
        //boardMoving.Volume = magnituge/10;
        count = Mathf.Round(magnituge / 15);

        if (oldCount < count)
        {
            for (int i = 0; i < (count - oldCount); i++)
            {                
                addToShaft(count + i);
            }
        }
        else if (oldCount > count && oldCount != count && shaft.Count > 0)
        {
            for (int i = 0; i < (oldCount - count); i++)
            {
                shaft.ElementAt(shaft.Count -1 ).Destroy();
                shaft.RemoveAt(shaft.Count -1 );
            }
        }
        if (magnituge < 3) RemoveChild(tip);
    }

    void GetVelocity()
    {
        Level level = ((MyGame)game).level;
        if (!level.marbles[0].Pull) velocity += level.marbles[0].Gravity * 20;
        velocity *= 0.99f;
        velocity.LimitLength(100);
    }

    public void Step()
    {
        magnituge = velocity.Length();
        oldCount = count;
        GetVelocity();
        Direction();
        Magnitude();
        UpdatePositons();
    }
}


