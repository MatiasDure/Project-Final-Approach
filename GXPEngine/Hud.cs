using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Hud:GameObject
{
    Button[] buttons;
    ScoreInfo scoreInfo;
    bool _paused = false;
    int pauseIndex;

    public bool Paused { get => _paused; } 

    public Hud(int pCurrentLevel, ScoreInfo pScore)
    {
        scoreInfo = pScore;
        buttons = new Button[] { new Button(pCurrentLevel, 1), 
                                new Button(-1, 2),
                                new Button(-1,3)};
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
}