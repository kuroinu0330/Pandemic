using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallsMove : MonoBehaviour
{

    Transform _riceBaby;
    //�ڕW�̃I�u�W�F�N�g
    //public string targetObjectName;
    //���ɂ���̃X�s�[�h
    public float _speed = 1f;

    GameObject _targetObject;
    // Start is called before the first frame update
    void Start()
    {
        _riceBaby = GameObject.FindGameObjectWithTag("kome").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector2.Distance(transform.position, _riceBaby.position) < 0.1f)
            return;*/
        transform.position = Vector2.MoveTowards
            (transform.position,
            new Vector2(_riceBaby.position.x, _riceBaby.position.y),
            _speed * Time.deltaTime);
    }
}
