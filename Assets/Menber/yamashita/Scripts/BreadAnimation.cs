using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadAnimation : MonoBehaviour
{
    // ��������I�u�W�F�N�g
    [SerializeField]
    private GameObject createPrefab;
    // �����͈�
    [SerializeField]
    private Transform rangeA;
    [SerializeField]
    private Transform rangeB;
    [SerializeField]
    private Transform rangeC;
    [SerializeField]
    private Transform rangeD;
    [SerializeField]
    private Transform rangeE;
    [SerializeField]
    private Transform rangeF;
    // ������
    private int numberOfObjectsMin = 1;
    private int numberOfObjectMiddle = 3;
    private int numberOfObjectsMax = 5;

    // ���������Ԋu
    private float interval = 0.4f;
    // �����Ԋu�̌�����
    private float intervalDecrement = 0.05f;
    // ���݂̐����Ԋu
    private float nowInterval;

    // �o�ߎ���
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nowInterval = interval;
        StartCoroutine(SpawnObjectsDelay());
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    private IEnumerator SpawnObjectsDelay()
    {
        while (true)
        {
            SpawnObjects();
            yield return new WaitForSeconds(nowInterval);
            nowInterval -= intervalDecrement;

            if (nowInterval < 0.05f)
            {
                nowInterval = 0.05f;
            }
        }
    }

    private void SpawnObjects()
    {
        if (time < 1.5f)
        {
            for (int i = 0; i < numberOfObjectsMin; i++)
            {
                // rangeA��rangeB�͈͓̔��Ń����_���Ȑ��l�𐶐�
                float x = Random.Range(rangeA.position.x, rangeB.position.x);
                float y = Random.Range(rangeA.position.y, rangeB.position.y);
                
                // �p������
                Instantiate(createPrefab, new Vector3(x, y), createPrefab.transform.rotation);
            }
        }
        else if (time < 3f)
        {
            for (int i = 0; i < numberOfObjectMiddle; i++)
            {
                // rangeC��rangeD�͈͓̔��Ń����_���Ȑ��l�𐶐�
                float x = Random.Range(rangeC.position.x, rangeD.position.x);
                float y = Random.Range(rangeC.position.y, rangeD.position.y);
                // ����
                Instantiate(createPrefab, new Vector3(x, y), createPrefab.transform.rotation);
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjectsMax; i++)
            {
                // rangeE��rangeF�͈͓̔��Ń����_���Ȑ��l�𐶐�
                float x = Random.Range(rangeE.position.x, rangeF.position.x);
                float y = Random.Range(rangeE.position.y, rangeF.position.y);
                // ����
                Instantiate(createPrefab, new Vector3(x, y), createPrefab.transform.rotation);
            }
        }
    }
}
