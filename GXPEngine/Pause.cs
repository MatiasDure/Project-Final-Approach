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

    public bool Paused { get => _paused; }

    public Pause(int pCurrentLevel):base("fullPauseBackground.png")
    {
        alpha = 255;
        buttons = new Button[] { new Button(-1, 2, "buttons/resume.png"),
                                new Button(pCurrentLevel, 1, "buttons/restart.png"),
                                new Button(pCurrentLevel, 1, "buttons/levels.png"),
                                new Button(-1, 3, "buttons/quit.png")};
        pausedButton = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            AddChild(buttons[i]);
            buttons[i].SetXY(width/2 - buttons[i].width/2, (i + 1) * 160);
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
