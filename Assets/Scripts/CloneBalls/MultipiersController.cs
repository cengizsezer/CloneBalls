using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipiersController : MonoBehaviour
{
    public int DublicateNumber;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.request();

    }

    private void OnTriggerEnter(Collider other)
    {
       

        if(other.gameObject.layer==8)
        {
            BallController otherBall = other.gameObject.GetComponent<BallController>();
            otherBall.CanCollide = true;
            if (!gameManager.DublicateBallList.Contains(otherBall))
            {

                gameManager.DublicateBallList.Add(otherBall);
                StartCoroutine(DublicateBall(otherBall, other.transform.position, DublicateNumber));
            }
            else { return; }
        }
    }


    IEnumerator DublicateBall(BallController ball, Vector3 Pos, int DublicateNumber)
    {
        for (int i = 0; i < DublicateNumber - 1; i++)
        {
            if (ball == null) yield break;

            BallController newball = Instantiate(ball, Pos, Quaternion.identity);
            gameManager.DublicateBallList.Add(newball);

            if (!gameManager.GameInBallList.Contains(newball))
            {
                gameManager.GameInBallList.Add(newball);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
