using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;
using Random = UnityEngine.Random;

public class BreadManager : MonoBehaviour 
{
    //パンのプレハブを保持する
    [SerializeField]
    private GameObject _breadBase;

    //パンの画像集
    [SerializeField] 
    private List<Sprite> _breadTextures;

    //シングルトン化
    public static BreadManager instance;
    
    private void Awake() {
        if (instance == null) 
        {
            instance = this;
        }
    }

    //パンの生成処理
    public void GenerateBread(Vector3 position) 
    {
        //乱数によってパンの見た目を変更する
        int typeNum = Random.Range(0, _breadTextures.Count);
        
        //パンの生成をする
        GameObject Bread = Instantiate(_breadBase, position, Quaternion.identity);
        
        //パンの見た目を変更する
        Bread.GetComponent<SpriteRenderer>().sprite = _breadTextures[typeNum];

    }
}
