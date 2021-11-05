using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Chronometer : MonoBehaviour
{
#if UNITY_EDITOR

    public bool RunOnGameplay = true;
    [UnityEngine.UI.Extensions.ReadOnly] public float Timer;

    private bool isActive;

    public bool LogOnScreen;

    private void Start()
    {
        if (RunOnGameplay)
        {
            GameController gameController = GameController.request();
            gameController.OnGameStarted.AddListener(StartChronometer);
            gameController.OnGameWin.AddListener(PauseChronometer);
            gameController.OnGameLost.AddListener(PauseChronometer);
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isActive)
        {
            Timer += Time.deltaTime;
        }
    }

    [Button("Start")]
    public void StartChronometer()
    {
        isActive = true;
    }

    [Button("Pause")]
    public void PauseChronometer()
    {
        isActive = false;
    }

    [Button("Stop")]
    public void StopChronometer()
    {
        Timer = 0;
        isActive = false;
    }

    void OnGUI()
    {
        if (LogOnScreen)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0.95f * h, w, h * 0.02f);
            style.alignment = TextAnchor.LowerLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            TimeSpan timeSpan = TimeSpan.FromSeconds(Timer);
            string timeText = string.Format("  {0:D2}:{1:D2}:{2:D2}:{3:D3}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            GUI.Label(rect, timeText, style);
        }
    }

#endif
}
