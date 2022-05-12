using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Button:AnimationSprite
{
    int target;
    int _id;
    public bool _paused = false;

    public bool Paused { get => _paused; }
    public Button(int pTarget = -1, int pId = 1, string pImgPath = "square.png", TiledObject obj = null) :base(pImgPath,2,1)
    {
        if (obj != null)
        {
            target = obj.GetIntProperty("target",0);
            _id = obj.GetIntProperty("id", 1);
        }
        else
        {
            _id = pId;
            target = pTarget;
            Console.WriteLine(target);
        }

    }

    public void Step()
    {
        bool clickedButton = Clicked();
        TypeButton(clickedButton);
    }

    void TypeButton(bool pClicked)
    {
        switch (_id)
        {
            case 1:
                LoadLvl(pClicked);
                break;
            case 2:
                PauseGame(pClicked);
                break;
            case 3:
                EndGame(pClicked);
                break;
            default:
                Console.WriteLine("not found ID");
                break;
        }
    }

    void LoadLvl(bool pClicked)
    {
        if(pClicked) ((MyGame)game).LoadLevel(target);
    }

    bool Clicked()
    {
        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            currentFrame = 1;
            if(Input.GetMouseButtonDown(0)) return true;
        }
        else currentFrame = 0;
        return false;
    }

    void PauseGame(bool pClicked)
    {
        if (pClicked) _paused = !_paused;
    }

    void EndGame(bool pClicked)
    {
        if (pClicked) ((MyGame)game).Destroy();
    }

}