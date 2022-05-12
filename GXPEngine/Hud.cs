using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Hud:GameObject
{
    Button[] buttons;
    Score scoreInfo;
    bool _paused = false;
    int pauseIndex;

    public bool Paused { get => _paused; } 

    public Hud(int pCurrentLevel, Score pScore)
    {
        scoreInfo = pScore;
        buttons = new Button[] { new Button(pCurrentLevel, 1), 
                                new Button(-1, 2)};
        pauseIndex = 1;
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
        _paused = buttons[pauseIndex].Paused;
        if(buttons[pauseIndex].Paused) buttons[pauseIndex]._paused = false;
    }

    void DisplayStars()
    {

    }
}