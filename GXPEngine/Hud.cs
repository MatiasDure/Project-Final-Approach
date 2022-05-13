using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Hud:GameObject
{
    Button[] buttons;
    Score score;
    ScoreInfo scoreInfo;
    bool _paused = false;
    int pauseIndex;
    AnimationSprite stars;
    AnimationSprite highScoreStars;
    Sprite highScoreLogo;
    int currentLevel;
    EasyDraw highscore;
    int amountStarsHighScore;

    public bool Paused { get => _paused; } 

    public Hud(int pCurrentLevel, Score pScore, ScoreInfo pScoreInfo)
    {
        score = pScore;
        scoreInfo = pScoreInfo;
        
        currentLevel = pCurrentLevel;
        buttons = new Button[] { new Button(pCurrentLevel, 1, "buttons/resetIcon.png"), 
                                new Button(-1, 2, "buttons/pause.png")};
        stars = new AnimationSprite("starSpritesheet.png", 1, 13);
        stars.SetXY(1450, 0); 
        AddChild(stars);
        highScoreStars = new AnimationSprite("starSpritesheet.png", 1, 13);
        highScoreStars.SetXY(1450, 550);
        AddChild(highScoreStars);
        highScoreLogo = new Sprite("highscore.png",false, false);
        AddChild(highScoreLogo);
        highScoreLogo.SetXY(1450,400);
        pauseIndex = 1;
        highscore = new EasyDraw(200,200,false);
        highscore.SetXY(0, 500);
        AddChild(highscore);
        for (int i = 0; i < buttons.Length; i++)
        {
            AddChild(buttons[i]);
            if (i == 0)
                buttons[i].x = 50;
            else buttons[i].x = 170;
            buttons[i].y = 50;
        }
        amountStarsHighScore = pScoreInfo.LevelScores[Mathf.Abs(currentLevel - 2)];
        DisplayHighScore();
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
        if(score.Stars == 2 && stars.currentFrame < 4) stars.Animate(0.1f);
        if(score.Stars == 1 && stars.currentFrame < 8) stars.Animate(0.1f);
        if(score.Stars == 0 && stars.currentFrame < 12) stars.Animate(0.1f);
    }

    void DisplayHighScore()
    {
        if (amountStarsHighScore == 2 && highScoreStars.currentFrame < 4) highScoreStars.currentFrame = 4;
        if (amountStarsHighScore == 1 && highScoreStars.currentFrame < 8) highScoreStars.currentFrame = 8;
        if (amountStarsHighScore == 0 && highScoreStars.currentFrame < 12) highScoreStars.currentFrame = 12;
    }
}