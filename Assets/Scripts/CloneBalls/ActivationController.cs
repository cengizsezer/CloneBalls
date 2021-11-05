using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    [Header("Bool")]
    public bool TriggerIsActive = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 8)
        {
            BallController ballController = other.gameObject.GetComponent<BallController>();
            TriggerIsActive = true;
            ballController.CanCollide = true;
        }
    }
}
