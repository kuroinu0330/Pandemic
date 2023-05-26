using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadAnimation : MonoBehaviour
{
    // 生成するオブジェクト
    [SerializeField]
    private GameObject createPrefab;
    // 生成範囲
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
    // 生成数
    private int numberOfObjectsMin = 1;
    private int numberOfObjectMiddle = 3;
    private int numberOfObjectsMax = 5;

    // 初期生成間隔
    private float interval = 0.4f;
    // 生成間隔の減少量
    private float intervalDecrement = 0.05f;
    // 現在の生成間隔
    private float nowInterval;

    // 経過時間
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
                // rangeAとrangeBの範囲内でランダムな数値を生成
                float x = Random.Range(rangeA.position.x, rangeB.position.x);
                float y = Random.Range(rangeA.position.y, rangeB.position.y);
                
                // パン生成
                Instantiate(createPrefab, new Vector3(x, y), createPrefab.transform.rotation);
            }
        }
        else if (time < 3f)
        {
            for (int i = 0; i < numberOfObjectMiddle; i++)
            {
                // rangeCとrangeDの範囲内でランダムな数値を生成
                float x = Random.Range(rangeC.position.x, rangeD.position.x);
                float y = Random.Range(rangeC.position.y, rangeD.position.y);
                // 生成
                Instantiate(createPrefab, new Vector3(x, y), createPrefab.transform.rotation);
            }
        }
        else
        {
            for (int i = 0; i < numberOfObjectsMax; i++)
            {
                // rangeEとrangeFの範囲内でランダムな数値を生成
                float x = Random.Range(rangeE.position.x, rangeF.position.x);
                float y = Random.Range(rangeE.position.y, rangeF.position.y);
                // 生成
                Instantiate(createPrefab, new Vector3(x, y), createPrefab.transform.rotation);
            }
        }
    }
}
