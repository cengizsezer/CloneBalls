using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEndController : MonoBehaviour
{

    GameManager gameManager;
    GameController gameController;

    private void Start()
    {
        gameManager = GameManager.request();
        gameController = GameController.request();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            BallController OtherBall = other.gameObject.GetComponent<BallController>();
            OtherBall.Entered = true;

            bool trueForAll = gameManager.GameInBallList.TrueForAll(x => x.Entered == true);
            Debug.Log(trueForAll);

            if (trueForAll)
            {
                bool WınGame = AllElementsAny(gameManager.GameInBallList);

                if(WınGame)
                {
                    gameController.WinGame();
                }else
                {
                    gameController.LostGame();
                }
            }
        }
    }

    private bool AllElementsAny(List<BallController> BallList)
    {
        bool allResponsesIncorrect = BallList.Any(x => x.Index == 2048);

        return  allResponsesIncorrect;
    }
}
