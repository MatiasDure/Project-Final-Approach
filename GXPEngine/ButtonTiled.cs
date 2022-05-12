using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class ButtonTiled : AnimationSprite
{
    int target;
    int _id;
    bool mainMenu = false;

    public ButtonTiled(TiledObject obj = null) : base("buttons/restart.png", 2, 1)
    {
        if (obj != null)
        {
            target = obj.GetIntProperty("target", 0);
            _id = obj.GetIntProperty("id", 1);
            mainMenu = obj.GetBoolProperty("main", false);
        }

        string path;
        if (_id == 3) path = "buttons/quit.png";
        else if (mainMenu) path = "buttons/start.png";
        else path = "buttons/level" + target + ".png";
        
        base._texture = new AnimationSprite(path, 2, 1).texture;
    }

    public void Update()
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
        if (pClicked) ((MyGame)game).LoadLevel(target);
    }

    bool Clicked()
    {
        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            currentFrame = 1;
            if (Input.GetMouseButtonDown(0)) return true;
        }
        else currentFrame = 0;
        return false;
    }

    void EndGame(bool pClicked)
    {
        if (pClicked) ((MyGame)game).Destroy();
    }

}