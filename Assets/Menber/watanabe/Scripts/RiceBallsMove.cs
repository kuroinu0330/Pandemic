using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallsMove : MonoBehaviour
{
    //目標のオブジェクト
    public string targetObjectName;
    //おにぎりのスピード
    public float _speed = 1f;

    GameObject _targetObject;
    // Start is called before the first frame update
    void Start()
    {
        _targetObject = GameObject.Find(targetObjectName);
    }

    // Update is called once per frame
    void Update()
    {
        //米との距離を調べる
        float dis = Vector2.Distance(this.transform.position, _targetObject.transform.position);
        Debug.Log("距離 ; " + dis);
        //一定の個数を獲得したら速度上昇
        //
        //
        //
        //
        //
    }
}
