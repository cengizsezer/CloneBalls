using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEditor;

public enum CameraStatus
{
    Stage1Camera,
    Stage2Camera,
    Stage3Camera,
    StartCamera,
    InGameCamera,
    WinCamera,
    LostCamera,
    None
}

public class CameraController : Singleton<CameraController>
{
    private const int ACTIVE_VALUE = 10;
    private const int PASSIVE_VALUE = 1;

    [System.Serializable]
    public class VirtualCamera
    {
        public CameraStatus CameraStatus;
        [Space]
        public CinemachineVirtualCamera Camera;
    }

    public VirtualCamera[] VirtualCameras;

    [HideInInspector] public CinemachineBrain Brain;

    public CameraStatus CurrentStatus { get; private set; }

    public Camera MainCamera { get; private set; }

    void Start()
    {
       
        CurrentStatus = CameraStatus.Stage1Camera;
        SetCameraStatus(CurrentStatus);

        MainCamera = Camera.main;
        Brain = MainCamera.GetComponent<CinemachineBrain>();

        GameController.Instance.OnGameStarted.AddListener((() => SetCameraStatus(CameraStatus.Stage1Camera)));
        GameController.Instance.OnGameStage2Change.AddListener((() => SetCameraStatus(CameraStatus.Stage2Camera)));
        GameController.Instance.OnGameStage3Change.AddListener((() => SetCameraStatus(CameraStatus.Stage3Camera)));

        //int EventNumber = GameController.Instance.OnGameStageChange.GetPersistentEventCount();
        //for (int i = 0; i < EventNumber; i++)
        //{
        //    Debug.Log(EventNumber.ToString());

        //}


    }

    [Button("Set Camera", ButtonStyle.Box)]
    public void SetCameraStatus(CameraStatus status)
    {
        foreach (var virtualCamera in VirtualCameras)
        {
            if (virtualCamera.CameraStatus == status)
            {
                virtualCamera.Camera.Priority = ACTIVE_VALUE;
            }
            else
            {
                virtualCamera.Camera.Priority = PASSIVE_VALUE;
            }
        }

        CurrentStatus = status;
    }

    /// <summary>
    /// Returns whether or not world position can be seen by main camera.
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public static bool IsPositionInCameraField(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        if (screenPos.x < Screen.width && screenPos.x > 0 && screenPos.y < Screen.height && screenPos.y > 0)
            return true;
        return false;
    }

}
