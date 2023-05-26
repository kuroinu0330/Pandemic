using System.Collections;
using System.Collections.Generic;
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
    public void CreateRiceBaby(Vector2 createPos)
    {
        //現在のクリエイトポイントがクリエイトコスト以下なら以下の処理を実行する
        if(_createEnergy >=_createCost)
        {
            //現在のクリエイトポイントからコストを差し引く
            _createEnergy -= _createCost;

            //米を生成する
            Instantiate(_riceBaby,new Vector3(createPos.x,createPos.y,0f),Quaternion.identity);
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
}
