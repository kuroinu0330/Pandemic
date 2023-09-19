using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SearchService;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;
using Random = UnityEngine.Random;

public class BreadManager : MonoBehaviour 
{
    //プレイヤーの情報を持っておく
    [SerializeField]
    private GameObject _player;
    
    //パンのプレハブを保持する
    [SerializeField]
    private GameObject _breadBase;
    
    //パンのプレハブを保持する
    [SerializeField]
    private GameObject _breadExtreem;
    
    //生成したパンの全てを保持する配列
    [SerializeField] 
    private List<NormalBreadEx> _breadObjects;

    [SerializeField] private List<NormalBreadEx> _isolationBread;

    //パンの行動開始ようBool
    private bool BreadsAllAction = false;
    
    //パンをまとめておく空のオブジェクト
    private GameObject EmptyObject;
    
    //パンの個体数を保持する変数
    private int _breadindividual = 0;

    //パンの個体数を保持する変数
    [SerializeField]
    private int _breadindividualEX = 0;
    
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
            EmptyObject = new GameObject(" BreadOffice");

        }
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        SortingBreadAarray(); 

        for (int i = 0; i < _breadObjects.Count; i++)
        {
            float dis = Vector3.Distance(_breadObjects[i].transform.position, _player.transform.position);

            if (dis > 2048 * 2f)
            {
                _breadObjects[i].GetComponent<NormalBreadEx>().ChangeReuseBreadPos(true);
            }
            else if(dis > 2048 * 2f)
            {
                _breadObjects[i].GetComponent<NormalBreadEx>().ChangeReuseBreadPos(false);
            }
        }
    }

    private void SortingBreadAarray()
    {
        //今世紀最大の気持ち悪コード
        _breadObjects.Sort((a, b) => MathF.Abs(Vector3.Distance(a.GameObject().transform.position, _player.transform.position))
            .CompareTo(MathF.Abs(Vector3.Distance(b.GameObject().transform.position, _player.transform.position))));  
    }

    /// <summary>
    /// リストから除外
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveListOOOOOOOOOOOOOOORU(GameObject obj)
    {
        _breadObjects.Remove(obj.GetComponent<NormalBreadEx>());
    }

    /// <summary>
    /// 特殊な方法で再利用対象オブジェクトに指定された際に実行する関数
    /// </summary>
    /// <param name="obj">再利用対象オブジェクトに指定された「Bread」オブジェクト</param>
    public void ReuseScheduleBread(GameObject obj)
    {
        obj.GetComponent<NormalBreadEx>().ChangeReuseBreadPos(true);
    }

    /// <summary>
    /// 再利用用のBreadを渡す関数
    /// (該当するオブジェクトがない場合現存するオブジェクトの中で一番プレイヤーから遠いオブジェクトが返される)
    /// </summary>
    /// <returns></returns>
    public GameObject ReuseBread()
    {
        NormalBreadEx breadEX = null;

        //SortingBreadAarray();
        for (int i = _breadObjects.Count-1; i > 0; i--)
        {
            breadEX = _breadObjects[i].GetComponent<NormalBreadEx>();
            if (breadEX.GetReuseBreadPos() && !breadEX.GetReuseBreadInterval())
            {
                breadEX.ChangeReuseBreadPos(false);
                breadEX.SetReuseAnt();
                StartCoroutine(breadEX.ReuseReset());
                return _breadObjects[i].GameObject();
            }
        }

        //SortingBreadAarray();
        for (int i = _breadObjects.Count-1; i > 0; i--)
        {
            breadEX = _breadObjects[i].GetComponent<NormalBreadEx>();
            if (!breadEX.GetReuseBreadPos() && !breadEX.GetReuseBreadInterval())
            {
                breadEX.ChangeReuseBreadPos(false);
                breadEX.SetReuseAnt();
                StartCoroutine(breadEX.ReuseReset());
                return _breadObjects[i].GameObject();
            }
        }

        return null;
    }

    //パンの生成処理
    public void GenerateBread(Vector3 position) 
    {
        //乱数によってパンの見た目を変更する
        //int typeNum = Random.Range(0, _breadTextures.Count);

        GameObject Bread = null;

        if (_breadindividual < 80)
        {
            //パンの生成をする
            Bread = Instantiate(_breadBase, position, Quaternion.identity);

            Bread.name = Bread.name + _breadindividual.ToString();

            _breadindividual++;

            _breadObjects.Add(Bread.GetComponent<NormalBreadEx>());

            Bread.transform.SetParent(EmptyObject.transform);

            // デバック
            //TestEnemyCount.instance.countUp();
            
            NormalBreadEx obj =Bread.GetComponent<NormalBreadEx>(); 
        
            obj.SetPlayerPosition(_player);
        
            obj.ActionOn();
        }
        else
        {
            Bread = ReuseBread();
            
            //Debug.Log(Bread.name);
            if (Bread != null)
            {
                Bread.transform.position = position;
                NormalBreadEx breadEx = Bread.GetComponent<NormalBreadEx>();
                StartCoroutine(breadEx.StopIt()); 
            }
        }
        
        //パンの見た目を変更する
        //Bread.GetComponent<SpriteRenderer>().sprite = _breadTextures[typeNum];

    }

    public void GenerateAbnormalBread(Vector3 position,Vector2 direction)
    {
        GameObject Bread = null;

        if (_breadindividualEX < 10)
        {
            Debug.Log("新型ブレッドエキスパンション");
            //パンの生成をする
            Bread = Instantiate(_breadExtreem, position, Quaternion.identity);

            StartCoroutine(Bread.GetComponent<AbNormalBreadEX>().Move(direction));

            _breadindividualEX++;

            Bread.name = Bread.name + "EX";

            Bread.transform.SetParent(EmptyObject.transform);
        }
    }

    public void BreadIndividualEXSub()
    {
        _breadindividualEX--;
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

    public Vector3 GetPlayerPosition()
    {
        return _player.transform.position;
    }
}
