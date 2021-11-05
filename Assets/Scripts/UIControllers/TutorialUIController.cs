using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIController : UIElement
{
    public override void Initialize()
    {
        base.Initialize();
        GameController.Instance.OnGameWin.AddListener(Deactivate);
        GameController.Instance.OnGameStarted.AddListener(GameStarted);

    }

    private void GameStarted()
    {
        //if (LevelManager.Instance.ActiveLevelData != null && LevelManager.Instance.ActiveLevelData.HasTutorial)
        //    Activate();
    }
}
