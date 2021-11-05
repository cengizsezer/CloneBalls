using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndUIController : UIElement
{
    public Button LevelFailButton;
    public Button LevelWinButton;


    public override void Initialize()
    {
        base.Initialize();
        GameController.OnGameLost.AddListener(LevelFailed);
        GameController.OnGameWin.AddListener(LevelWin);
    }

    public override void SetActive(bool activate)
    {
        base.SetActive(activate);

        if (!activate)
        {
            LevelFailButton.gameObject.SetActive(false);
            LevelWinButton.gameObject.SetActive(false);
        }
    }

    public void LevelWin()
    {
        Activate();
        LevelWinButton.gameObject.SetActive(true);
    }


    public void LevelFailed()
    {
        Activate();
        LevelFailButton.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        Deactivate();
        GameController.IncreaseLevel();
    }

    public void RestartLevel()
    {
        Deactivate();
        GameController.ReloadLevel();

    }

    public override void PrepareForBuild()
    {
        base.PrepareForBuild();
        LevelFailButton.gameObject.SetActive(false);
        LevelWinButton.gameObject.SetActive(false);
    }
}
