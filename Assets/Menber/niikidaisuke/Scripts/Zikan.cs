using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Zikan : MonoBehaviour
{
    private float counttime = 0.0f;//���Ԃ��͂���
    public float timeLimit = 30.0f;//��������

    void Start()
    {

    }

    void Update()
    {
        counttime += Time.deltaTime;//�}�C�t���[�����ɂ����������Ԃ𑫂��Ă���


        if (counttime > timeLimit)
        {
            SceneManager.LoadScene("rizaruto");//�w�肵�����Ԃ��߂�����V�[���J�ځB("")�̒��ɑJ�ڐ�̃V�[���̖��O�������B
        }

    }
}