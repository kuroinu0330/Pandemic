using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteStaminaRiceBaby : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            RiceBabyCreateManager.Instance.InfiniteStaminaAcquisition();
            Destroy(this.gameObject);
            Debug.Log("Go!On!!");
        }
    }
}
