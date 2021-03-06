using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Pause:Sprite
{
    Button[] buttons;
    bool _paused;
    int pausedButton;
    Sprite pauseImg;

    public bool Paused { get => _paused; }

    public Pause(int pCurrentLevel):base("fullPauseBackground.png")
    {
        alpha = 255;
        buttons = new Button[] { new Button(-1, 2, "buttons/resume.png"),
                                new Button(pCurrentLevel, 1, "buttons/restart.png"),
                                new Button(1, 1, "buttons/level1.png"),
                                new Button(0, 1, "buttons/level0.png")};
        pausedButton = 0;

        float halfPause = width / 2;

        pauseImg = new Sprite("pause.png",false,false);
        AddChild(pauseImg);
        pauseImg.SetXY(halfPause - pauseImg.width/2 + 3,50);
        for (int i = 0; i < buttons.Length; i++)
        {
            AddChild(buttons[i]);
            buttons[i].SetXY(halfPause - buttons[i].width/2, (i + 1) * 160 + 100);
        }
        SetXY(game.width/2 - this.width/2,game.height/2 - this.height/2);
    }

    public void Step()
    {
        if(alpha > 110) alpha-=5;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Step();
        }
        _paused = !buttons[pausedButton].Paused;
    }
}
