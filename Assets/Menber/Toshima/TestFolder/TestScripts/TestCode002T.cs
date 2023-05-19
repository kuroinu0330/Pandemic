using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCode002T : MonoBehaviour
{
    //クリエイトポイントが正常に可視化されるか確認するためのテストコード
    private Slider slider;

    void Start() 
    {
        slider = this.gameObject.GetComponent<Slider>();

        slider.maxValue = RiceBabyCreateManager.Instance.GetMaxCreateEnergy();    
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = RiceBabyCreateManager.Instance.CreateEnergyUpdate();
    }
}
