using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteStaminaRiceBaby : MonoBehaviour
{
    public InfinitStaminaUI infinitStaminaUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            infinitStaminaUI.InfinitStaminaUITrue();
            RiceBabyCreateManager.Instance.InfiniteStaminaAcquisition();
            Destroy(this.gameObject);
            Debug.Log("Go!On!!");
        }
        else { infinitStaminaUI.InfinitStaminaUIfalse(); }
    }
}
