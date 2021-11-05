using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType { Stage1, Stage2, Stage3 }

public class BallController : MonoBehaviour
{
    [Header("Ball Is Index")]
    public int Index;
    [Header("Control Booleans")]
    public bool CanCollide = false;
    public bool Entered = false;
    GameManager gameManager;
    GameController gameController;
    [Header("Component")]
    public SpawnController spawnController;
    public MeshRenderer BallMeshRenderer;
    [Header("STAGE")]
    public StageType stageType;
    [Space]
    [Header("Candidate")]
    [SerializeField]private BallController CandidateBall;
    public BallController _CandidateBall
    {
        get
        {
            return CandidateBall;
        }
        
        set
        {
            if(_CandidateBall.CandidateBall==null)
            {
                
                CandidateBall = value;
            }
            else
            {
                Debug.Log("candidateball null değildir.");
            }
            
        }
    }
    public Material[] BallMaterial = new Material[11];

    private void Start()
    {
        CanCollide = false;
        Entered = false;
        gameManager = GameManager.request();
        gameController = GameController.request();
    }


    private void OnTriggerEnter(Collider other)
    {
        StageEndController otherTrigger = other.gameObject.GetComponent<StageEndController>();

        if (other.gameObject.layer == 12)
        {

            stageType = StageType.Stage1;

            if (otherTrigger.AllTrue == true) { gameController.Stage2Change(); }
        }

        if (other.gameObject.layer == 13)
        {
            stageType = StageType.Stage2;

            if (otherTrigger.AllTrue == true) { gameController.Stage3Change(); }
        }
       

    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.layer==8)
        {
            if(this.Index==other.gameObject.GetComponent<BallController>().Index)
            {
                CandidateBall = other.gameObject.GetComponent<BallController>();
                other.gameObject.GetComponent<BallController>().CandidateBall = this;
            }
            else { return; }

            StartCoroutine(MergeBall());

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer==8)
        {

            if(gameObject.GetComponent<BallController>().CandidateBall == collision.gameObject.GetComponent<BallController>())
            {
                CandidateBall.CandidateBall = null;
                CandidateBall = null;
                
            }

        }

    }

    IEnumerator MergeBall()
    {
        yield return new WaitForSeconds(0.3f);
       
        if (CandidateBall !=null)
        {

            if ( CandidateBall.CandidateBall == this && this.Index == CandidateBall.Index && Index < 2048 && CanCollide == true)
            {
                
                this.transform.DOScale(transform.localScale * 1.2f, .2f);
                gameObject.GetComponent<BallController>().Index = Index * 2;
                gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = BallMaterial[(int)Mathf.Log(Index, 2) - 1];
                Destroy(CandidateBall.gameObject);
                gameManager.GameInBallList.Remove(CandidateBall);
                gameManager.DublicateBallList.Remove(CandidateBall);
            }
        }
        
    }
}
