using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallsMove : MonoBehaviour
{
    //�ڕW�̃I�u�W�F�N�g
    public string targetObjectName;
    //���ɂ���̃X�s�[�h
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
        //�ĂƂ̋����𒲂ׂ�
        float dis = Vector2.Distance(this.transform.position, _targetObject.transform.position);
        Debug.Log("���� ; " + dis);
        //���̌����l�������瑬�x�㏸
        //
        //
        //
        //
        //
    }
}
