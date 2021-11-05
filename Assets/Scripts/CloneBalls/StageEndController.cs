using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class StageEndController : MonoBehaviour
{
    GameManager gameManager;
    GameController gameController;
    CameraController cameraController;
    
    [Header("GAME OBJECTS")]
    public GameObject Fracture;
    public GameObject Target;
    [Space]
    [Header("MATERİAL LİST")]
    public Material[] MaterialList = new Material[11];
    [Space]
    [Header("FRACTURE RİGİDBODY LİST")]
    public Rigidbody[] FractureRbArray;
    public bool AllTrue = false;
   

    private void Awake()
    {
        FractureRbArray = Fracture.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < FractureRbArray.Length; i++)
        {
            FractureRbArray[i].isKinematic = true;
        }
    }

    private void Start()
    {
        gameManager = GameManager.request();
        cameraController = CameraController.request();
        gameController = GameController.request();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==8)
        {
            other.gameObject.GetComponent<BallController>().Entered = true;


            if (gameManager.GameInBallList.TrueForAll(i => i.Entered == true))
            {
                AllTrue = true;
            }
        }
       
    }


    private void OnTriggerStay(Collider other)
    {
        
       
        if(other.gameObject.layer==8)
        {
            int max = 0;

            for (int i = 0; i < gameManager.GameInBallList.Count; i++)
            {
                int index = gameManager.GameInBallList[i].gameObject.GetComponent<BallController>().Index;
                if (other.gameObject.GetComponent<BallController>().Entered==true)
                {
                    if (max < index)
                    {
                        max = index;
                        
                    }
                }
               
            }



            if (max>=32)
            {
               
                Target.transform.GetChild(0).GetComponent<MeshRenderer>().material = MaterialList[(int)Mathf.Log(max, 2) - 1];

                if (AllTrue==true)
                {

                    DOVirtual.DelayedCall(3, () =>
                    {
                        Destroy(Target);
                        for (int i = 0; i < FractureRbArray.Length; i++)
                        {
                            FractureRbArray[i].isKinematic = false;
                        }

                    });
                    DOVirtual.DelayedCall(1, () =>
                    {
                        Fracture.SetActive(false);

                    });

                    //for (int i = 0; i < gameManager.GameInBallList.Count; i++)
                    //{
                    //    gameManager.GameInBallList[i].Entered = false;
                    //}

                    gameManager.GameInBallList.ForEach(i => i.Entered = false);
                    AllTrue = false;
                    gameManager.DublicateBallList.Clear();
                   
                }
               

            }
           

        }
        
    }
}
