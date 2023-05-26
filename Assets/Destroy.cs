using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// 対象のゲームオブジェクトを破棄するスクリプトです。
/// </Summary>
public class Destroy : MonoBehaviour
{
    // 破棄するオブジェクトへの参照を保持します。
    public GameObject targetObj;

    void Start()
    {
        // 引数のGameObjectを破棄します。
        Destroy(targetObj);
    }
}