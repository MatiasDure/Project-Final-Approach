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
    AnimationSprite stars;

    public bool Paused { get => _paused; } 

    public Hud(int pCurrentLevel, Score pScore)
    {
        scoreInfo = pScore;
        buttons = new Button[] { new Button(pCurrentLevel, 1, "buttons/resetIcon.png"), 
                                new Button(-1, 2, "buttons/pause.png")};
        stars = new AnimationSprite("starSpritesheet.png", 1, 13);
        stars.SetXY(1500, 0); 
        AddChild(stars);
        pauseIndex = 1;
        for (int i = 0; i < buttons.Length; i++)
        {
            AddChild(buttons[i]);
            if (i == 0)
                buttons[i].x = 50;
            else buttons[i].x = 170;
            buttons[i].y = 50;
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
        DisplayStars();
    }

    void DisplayStars()
    {
        if(scoreInfo.Stars == 2 && stars.currentFrame < 4) stars.Animate(0.1f);
        if(scoreInfo.Stars == 1 && stars.currentFrame < 8) stars.Animate(0.1f);
        if(scoreInfo.Stars == 0 && stars.currentFrame < 12) stars.Animate(0.1f);

    }
}