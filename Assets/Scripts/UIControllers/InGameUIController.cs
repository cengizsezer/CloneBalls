using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIController : UIElement
{
    public Button MenuButton;
    public override void Initialize()
    {
        base.Initialize();

        GameController.OnGameStarted.AddListener(Activate);
        GameController.OnGameLost.AddListener(Deactivate);
        GameController.OnGameWin.AddListener(Deactivate);

        if (!GameController.IsGameStarted)
        {
            Activate();
            MenuButton.onClick.AddListener(GameController.Pause);
        }
        


    }
}
