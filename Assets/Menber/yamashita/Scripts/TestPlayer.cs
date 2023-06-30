using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �������̎擾
        float x_raw = Input.GetAxis("Horizontal");
        float y_raw = Input.GetAxis("Vertical");
        // �ړ�
        transform.Translate(x_raw * _speed * Time.deltaTime, y_raw * _speed * Time.deltaTime, 0);
    }


}
