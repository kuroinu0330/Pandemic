using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SearchService;
using Random = UnityEngine.Random;

public class BreadManager : MonoBehaviour 
{
    //プレイヤーの情報を持っておく
    [SerializeField]
    private GameObject _player;
    
    //パンのプレハブを保持する
    [SerializeField]
    private GameObject _breadBase;
    
    //生成したパンの全てを保持する配列
    [SerializeField] 
    private List<GameObject> _breadObjects;
    
    //パンの行動開始ようBool
    private bool BreadsAllAction = false;

    //パンの画像集
    //[SerializeField] 
    //private List<Sprite> _breadTextures;

    //シングルトン化
    public static BreadManager Instance;
    
    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
    }

    //パンの生成処理
    public void GenerateBread(Vector3 position) 
    {
        //乱数によってパンの見た目を変更する
        //int typeNum = Random.Range(0, _breadTextures.Count);
        
        //パンの生成をする
        GameObject Bread = Instantiate(_breadBase, position, Quaternion.identity);
        
        _breadObjects.Add(Bread);
        
        NormalBreadEx obj =Bread.GetComponent<NormalBreadEx>(); 
        
        obj.SetPlayerPosition(_player);
        
        obj.ActionOn();

        //パンの見た目を変更する
        //Bread.GetComponent<SpriteRenderer>().sprite = _breadTextures[typeNum];

    }
    
    //パンの一斉行動開始処理
    public void BreadsActionOn()
    {
        // for (int i = 0; i < _breadObjects.Count; i++)
        // {
        //     _breadObjects[i].GetComponent<NormalBreadEx>().ActionOn();
        // }
        BreadsAllAction = true;
    }

    public void BreadGoOn()
    {
        for (int i = 0; i < _breadObjects.Count; i++)
        {
            _breadObjects[i].GetComponent<NormalBreadEx>().ActionOn();
        }
    }
    public void BreadGoOff()
    {
        for (int i = 0; i < _breadObjects.Count; i++)
        {
            _breadObjects[i].GetComponent<NormalBreadEx>().ActionOff();
        }
    } 
    //パンの一斉行動停止処理
    public void BreadsActionOff()
    {
        // for (int i = 0; i < _breadObjects.Count; i++)
        // {
        //     _breadObjects[i].GetComponent<NormalBreadEx>().ActionOff();
        // }
        BreadsAllAction = false;
    }

    public bool GetActionBool()
    {
        return BreadsAllAction;
    }

    public int GetBreadsCount()
    {
        return _breadObjects.Count;
    }

    public void TouchPlayer()
    {
        SoundManager.instance.AllMute();
        SceneManager.LoadScene("Result");
    }

    public void SpeedRankUpMagic(int rank)
    {
        for (int i = 0; i < _breadObjects.Count; i++)
        {
            _breadObjects[i].GetComponent<NormalBreadEx>().MoveSpeedUpRank(rank);
        }
    }
}
