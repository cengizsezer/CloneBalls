using DG.Tweening;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   

    private void Start()
    {
        DOTween.Init();
        LeanTouch.OnFingerDown += OnFingerDown;

    }


    private void OnDestroy()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
    }

    public void OnFingerDown(LeanFinger finger)
    {

        if (finger.IsOverGui == false)
        {

            Ray ray = finger.GetRay();
            RaycastHit hit;
            int LayerMask = 10;
            if (Physics.Raycast(ray, out hit, LayerMask) == true)
            {
                GameObject pın = hit.transform.GetComponent<PinController>().gameObject;
                Vector3 pınTs = hit.transform.GetComponent<PinController>().transform.position;
                

                if (pın)
                {
                    float x = hit.transform.GetComponent<PinController>().PosX;
                    float y = hit.transform.GetComponent<PinController>().PosY;
                    hit.transform.GetComponent<PinController>().gameObject.transform.
                        DOMove(new Vector3(x,y,pınTs.z), 1);
                    
                    
                }
            }
        }

    }
}
