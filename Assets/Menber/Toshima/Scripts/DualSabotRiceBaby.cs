using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualSabotRiceBaby : MonoBehaviour
{
    public DualSabotUI dualSabotUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            dualSabotUI.DualSabotUITrue();
            RiceBabyCreateManager.Instance.RiceBabyDualSabotItemAcquisition();
            
            // ここに点滅アニメーションのスタート処理を書く

            
            Destroy(this.gameObject);
            Debug.Log("Go!On!!");
        }
        else { dualSabotUI.DualSabotUIfalse(); }
    }
}
