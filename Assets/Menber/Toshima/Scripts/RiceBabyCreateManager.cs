using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class RiceBabyCreateManager : MonoBehaviour
{
    //米粒のプレハブ
    [SerializeField]
    private GameObject _riceBaby;

    //最大クリエイトポイント(最大スタミナ)
    [SerializeField]
    private float _maxCreateEnergy;

    //現在のクリエイトポイント(現在のスタミナ)
    [SerializeField]
    private float _createEnergy;

    //米の生成に必要なコスト
    [SerializeField]
    private float _createCost;

    //クリエイトポイントの回復効率
    [SerializeField]
    private float _recoveryEfficiencyCreateEnergy;

    //プレイヤー操作と兼ね合いをとるシングルトン処理
    public static RiceBabyCreateManager Instance;

    //米粒の生成数が上昇していることを示すbool型変数
    private bool _dualSabotRiceBaby = false;
    
    //米粒も生成数上昇アイテムを取得した時にtrueになる
    private bool _dealSabotRiceBabyReady = false;
    
    //米粒の生成コストが減少していることを示すbool型変数
    private bool _createCostReduction = false;
    
    //米粒の生成コストを減少させるアイテムを取得したときにtrueになる
    private bool _createCostReductionReady = false;



    void Awake() 
    {       
        //初期化処理
        Initializ();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //クリエイトポイントを最大値以下なら以下の処理を実行する
        if(_createEnergy < _maxCreateEnergy)
        {
            //クリエイトポイントを毎秒1ポイントのペースで回復させる
            _createEnergy += 1.0f * _recoveryEfficiencyCreateEnergy * Time.deltaTime;
        }
        //クリエイトポイントが最大値を超えた場合以下の処理を実行する
        else if(_createEnergy >= _maxCreateEnergy)
        {
            //クリエイトポイントを最大値で上書きする
            _createEnergy = _maxCreateEnergy;
        }
    }

    private void Initializ()
    {
        //シングルトン化
        if(Instance == null)
        {
            Instance = this;
        }

        //クリエイトポイントを最大まで回復させる
        _createEnergy = _maxCreateEnergy;
    }

    /// <summary>
    /// 米粒の生成処理
    /// </summary>
    /// <param name="createPos"></param>
    public void CreateRiceBaby(Vector2 createPos,int num)
    {
        switch(num)
        {
            case 0:
            //現在のクリエイトポイントがクリエイトコスト以下なら以下の処理を実行する
            if(_createEnergy >=_createCost || _createCostReduction)
            {
                if (!_createCostReduction)
                {
                    //現在のクリエイトポイントからコストを差し引く
                    _createEnergy -= _createCost; 
                }
                
                SoundManager.instance.PlaySE(1);

                if (_dualSabotRiceBaby)
                {
                    //一つ目の米を生成する
                    Instantiate(_riceBaby,new Vector3(createPos.x - 60f,createPos.y,0f),Quaternion.identity); 
                    //二つ目の米を生成する
                    Instantiate(_riceBaby,new Vector3(createPos.x + 60f,createPos.y,0f),Quaternion.identity); 
                }
                else
                {
                    //米を生成する
                    Instantiate(_riceBaby,new Vector3(createPos.x,createPos.y,0f),Quaternion.identity);  
                }
            }
            break;
            case 1:
                //米を生成する
                Instantiate(_riceBaby,new Vector3(createPos.x,createPos.y,0f),Quaternion.identity);
            break;
        }
        
        
    }

    /// <summary>
    /// UI表示用に最大クリエイトポイントを返す関数(
    /// </summary>
    /// <returns>現在のクリエイトポイント</returns>
    public float GetMaxCreateEnergy()
    {
        return _maxCreateEnergy;
    }

    /// <summary>
    /// UI表示用に現在のクリエイトポイントを返す関数
    /// </summary>
    public float CreateEnergyUpdate()
    {
        return _createEnergy;
    }

    /// <summary>
    /// 米粒の生成量を上昇させるアイテムを獲得した際に呼ばれる関数
    /// </summary>
    public void RiceBabyDualSabotItemAcquisition()
    {
        _dealSabotRiceBabyReady = true;
        StartCoroutine(RiceBabyDualSabotSystem());
    }

    /// <summary>
    /// 米粒の生成量を上昇させるアイテムの効果時間を測定するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator RiceBabyDualSabotSystem()
    {
        if (_dualSabotRiceBaby)
        {
            yield break;
        }
        else
        {
            _dealSabotRiceBabyReady = false;
        }

        //15
        float timeLimit = 5f;

        float timer = 0f;
        
        _dualSabotRiceBaby = true;

        while (timeLimit >= timer)
        {
            timer += Time.deltaTime;
            
            //Debug.Log(timer);

            if (_dealSabotRiceBabyReady)
            {
                timer = 0f;
                _dealSabotRiceBabyReady = false;
            }

            yield return null;
        }

        _dualSabotRiceBaby = false;
        
        yield break;
    }
    
    /// <summary>
    /// 米粒の生成量を上昇させるアイテムを獲得した際に呼ばれる関数
    /// </summary>
    public void InfiniteStaminaAcquisition()
    {
        _createCostReductionReady = true;
        StartCoroutine(InfiniteStaminaSystem());
    }

    /// <summary>
    /// 米粒の生成量を上昇させるアイテムの効果時間を測定するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator InfiniteStaminaSystem()
    {
        if (_createCostReduction)
        {
            yield break;
        }
        else
        {
            _createCostReductionReady = false;
        }
        
        float timeLimit = 15f;

        float timer = 0f;
        
        _createCostReduction = true;

        while (timeLimit >= timer)
        {
            timer += Time.deltaTime;
            
            Debug.Log(timer);

            if (_createCostReductionReady)
            {
                timer = 0f;
                _createCostReductionReady = false;
            }

            yield return null;
        }

        _createCostReduction = false;
        
        yield break;
    }
}
