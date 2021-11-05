using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public BallController BallPrefeb;
    public Transform spawnPoint;
    GameManager gameManager;
    BallController ballController;
    public bool CollisionAllowed = false;

    private void Start()
    {
        gameManager = GameManager.request();

        if(gameManager.StageList[0].activeInHierarchy)
        {
           StartCoroutine(InıtSpawnBall(BallPrefeb,spawnPoint));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==8)
        {
            CollisionAllowed = false;
        }
        else { CollisionAllowed = true; }
       
    }

    IEnumerator InıtSpawnBall(BallController BallPrefeb,Transform spanPoint)
    {
        for (int i = 0; i < 10; i++)
        {
            BallController newBall = Instantiate(BallPrefeb, spawnPoint.position, Quaternion.identity) as BallController;
            newBall.Index = 2;
            gameManager.GameInBallList.Add(newBall);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
