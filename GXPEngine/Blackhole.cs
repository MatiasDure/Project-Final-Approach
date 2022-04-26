using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Blackhole:Ball
{
    Vector2 position;
    public Blackhole(Vector2 pPos) :base("circle.png",1,1, pPos)
    {
        position = pPos;
    }

    void update()
    {
        Level myLevel = ((MyGame)game).currentLevel;
        foreach (var item in myLevel.item)
        {
            if(item != this)
            {
                Vector2 difference = item.position - position;    
                float distance = difference.Length();

                int detectionRange = 400; 
                float gravitationalPull = -Mathf.Pow((0.01f * distance - (detectionRange / 100)), 3.4f) * (Mathf.Pow(0.01f*detectionRange-4,2)*0.35f+0.15f);

                float G = 6.67f * Mathf.Pow(10, -11);
                int Mass = 20;
                float gravitationalPull2 = G * (Mass * Mathf.Pow(10, 13) / Mathf.Pow(distance, 2));

                item.velocity += difference.Normalized() * gravitationalPull;

            }
        }
    }
}
