using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary>
/// �Ώۂ̃Q�[���I�u�W�F�N�g��j������X�N���v�g�ł��B
/// </Summary>
public class Destroy : MonoBehaviour
{
    // �j������I�u�W�F�N�g�ւ̎Q�Ƃ�ێ����܂��B
    public GameObject targetObj;

    void Start()
    {
        // ������GameObject��j�����܂��B
        Destroy(targetObj);
    }
}