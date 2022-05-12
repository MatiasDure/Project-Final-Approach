using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Hud:GameObject
{
    Button[] buttons;
    bool _paused = false;

    public bool Paused { get => _paused; } 

    public Hud(int pCurrentLevel)
    {
        buttons = new Button[] { new Button(pCurrentLevel, 1), 
                                new Button(pCurrentLevel+1, 1), 
                                new Button(pCurrentLevel-1, 1),
                                new Button(-1, 2),
                                new Button(-1,3)};
        for (int i = 0; i < buttons.Length; i++)
        {
            AddChild(buttons[i]);
            buttons[i].x += i * 100;
        }
    }

    public void Step()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Step();
        }
        _paused = buttons[3].Paused;
        if(buttons[3].Paused) buttons[3]._paused = false;
    }
}