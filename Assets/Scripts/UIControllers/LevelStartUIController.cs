using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartUIController : UIElement
{
    public override void Initialize()
    {
        base.Initialize();
        GameController.Instance.OnNewLevelLoaded.AddListener(Activate);
        GameController.Instance.OnGameStarted.AddListener(Deactivate);
    }

    private void Update()
    {
        if (isActive)
        {
#if UNITY_EDITOR

            if (Input.GetMouseButtonDown(0)) GameController.Instance.StartGame();
#else
       if(Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began) GameController.Instance.StartGame();
#endif

        }
    }
}
