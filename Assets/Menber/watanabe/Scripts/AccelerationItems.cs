using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationItems : MonoBehaviour
{ 
    //�X�s�[�h�̕ϐ�
    public static float _itemSpeed = 1.0f;

    //n秒間加速させる時間
    [SerializeField]
    private float _AccelerationTime;

    public static AccelerationItems Item;

    
    public IEnumerator Acceleration()
    {
        float _Time = 0.0f;
        yield return null;
        while (true)
        {
            _Time += Time.deltaTime;
            if (_AccelerationTime <= _Time)
            {
                //GetComponent<Renderer>().material.color = new Color32(250, 150, 40, 255);
                _itemSpeed = 1.0f;
                yield break;
            }
            yield return null;
        }
    }
    private void Awake()
    {
       Item = this.gameObject.GetComponent<AccelerationItems>();
    }
}
