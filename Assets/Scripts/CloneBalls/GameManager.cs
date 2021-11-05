using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<BallController> GameInBallList;
    public List<GameObject> StageList;
    public List<BallController> DublicateBallList;

    private void Start()
    {
        GameInBallList = new List<BallController>();
        StageList = new List<GameObject>();
        DublicateBallList = new List<BallController>();
    }


}
