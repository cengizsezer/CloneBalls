using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPauseUIController : UIElement
{
    public Button LevelResumeButton;
    public Button LevelMenuButton;
    public Button LevelQuitButton;

    //public List<Button> Buttons;


    public override void Initialize()
    {
        base.Initialize();

        GameController.OnGamePaused.AddListener(Activate);
        GameController.OnGameResume.AddListener(Deactivate);
        LevelResumeButton.onClick.AddListener(()=>
        {
            GameController.Resume();
            Deactivate();
            
            
        });

        LevelMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            Deactivate();
            SceneManager.LoadScene("MENU");
        });

        LevelQuitButton.onClick.AddListener(() =>
        {
            Deactivate();
            Debug.Log("Quitting Game...");
            Application.Quit();
        });

    }
   
}
