using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skip : MonoBehaviour
{
    private int _SkipCount;

    [SerializeField]
    private GameObject _RiceBabyCount;
    
    public void Click()
    {
        _SkipCount++;
        switch (_SkipCount)
        {
            case 1:
                Debug.Log("一個目生成");
                Instantiate(_RiceBabyCount, new Vector3(-759f, 1100f, 0), Quaternion.identity);
                //StartCoroutine(createobj());
                break;
            case 2:
                Debug.Log("二個目生成");
                Instantiate(_RiceBabyCount, new Vector3(-554f, 1100f, 0), Quaternion.identity);
                break;
            case 3:
                Debug.Log("三個目生成");
                Instantiate(_RiceBabyCount, new Vector3(-348, 1100, 0), Quaternion.identity);
                break;
            case 4:
                Debug.Log("四個目生成");
                Instantiate(_RiceBabyCount, new Vector3(-133, 1100, 0), Quaternion.identity);
                break;
            case 5:
                Debug.Log("五個目生成");
                Instantiate(_RiceBabyCount, new Vector3(60f, 1100f, 0), Quaternion.identity);
                Debug.Log("Titleに移動");
                SceneManager.LoadScene("Title"); 
                break;
        }
        /*IEnumerator createobj()
        {
            float _Time = 0.0f;
            yield return null;
            while (true)
            {
                _Time += Time.deltaTime;
                if (5 <= _Time)
                {
                    _SkipCount = 0;
                    Destroy(_RiceBabyCount);
                    yield break;
                }
            }
            yield return null;
        }*/
    }

 
}

