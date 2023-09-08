using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using TMPro;
using Unity.Mathematics;

public class PlayerInputManagement : MonoBehaviour
{
    //入力判定用のデッドゾーン
    [SerializeField]
    private float _playerInputDeadzone = 500f;

    //米粒の半径
    [SerializeField]
    private float _riceBabyRadius = 500;

    // //米粒のプレハブ(別ソースコードに転移)
    // [SerializeField]
    // private GameObject _riceBaby;

    // //当たり判定を取得するためのプレハブ
    // [SerializeField]
    // private GameObject _riceBabyPhantomPrefab;

    // //生成したプレハブの実体をいじるための変数
    // [SerializeField]
    // private GameObject _riceBabyPhantomObject;

    [SerializeField] private GameObject _perticleObject;

    //プレイヤーがタッチを始めた最初の座標
    [SerializeField]
    private Vector2 _originalTouchPosition = Vector2.zero;

    //プレヤーの操作が「タッチ」か「フリック」かを保持する
    [SerializeField]
    private InputType _inputType = InputType.None;

    //現在プレイヤーの入力による処理を受け付けるかの変数
    [SerializeField]
    private bool _actionReady = false;

    //現在入力中化を判断する変数
    [SerializeField]
    private bool _touchNow = false;

    //生成処理との兼ね合いをとるシングルトン処理
    public static PlayerInputManagement Instance;

    //入力方法の一覧
    private enum InputType
    {
        None,
        Tap,
        Flick
    }

    void Awake()
    {
        //シングルトン化
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start() 
    {

        //_riceBabyPhantomObject = GameObject.Instantiate(_riceBabyPhantomPrefab,Vector2.zero,Quaternion.identity);

        //デバック用の強制許可処理
        //InputSwitching();
        //PlayerActionOn();
    }

    void Update()
    {
        //プレイヤーの操作を受け付ける関数
        InputTypejudgement();
    }

    //プレイヤーの入力が「タッチ」か「フリック」かを判断する関数
    private void InputTypejudgement()
    {   
        //エディターで起動時に分岐(マウス操作)
        if(Application.isEditor)
        {
            //クリックの開始時に以下の処理を実行
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                //プレイヤーの操作を受け付けている最中
                _touchNow = true;

                //現在の入力タイプをタップとする
                _inputType = InputType.Tap;

                //現在のマウス座標を取得
                Vector2 mousePos = Input.mousePosition;

                //_originalTouchPositionにタッチ開始時のカーソル座標を保存する(PCの画面上の座標が出たため改善)
                //_originalTouchPosition = new Vector2(mousePos.x
                //                                    ,mousePos.y);

                //スクリーン上のマウス座標をタッチ開始時のカーソル座標として保存する
                _originalTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, 
                                                                                    mousePos.y,10f));

                CreatePerticle(_originalTouchPosition);
                
                //生成処理を実行する
                CreateAction(_originalTouchPosition);

                //米を生成する(別ソースコードに転移)
                //CreateRiceBaby(_originalTouchPosition);                
            }

            //　プレイヤーの操作受け付け中に以下の処理を実行する
            if(_touchNow)
            {
                //現在のマウス座標を取得
                Vector2 mousePos = Input.mousePosition;

                //マウスの座標をスクリーン上に置き換えたものに加工
                Vector2 mousePosOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, 
                                                                                    mousePos.y,10f));

                //タッチ開始時のカーソル座標とタッチ終了時のカーソル座標を引いて距離を求める
                Vector2 PosForCulc = _originalTouchPosition - mousePosOnScreen;

                //タッチ開始(プレイヤーの操作受け付け開始)からカーソルがデッドゾーン外に出た時点で「フリック」とする
                if((Mathf.Abs(PosForCulc.x) >= _playerInputDeadzone || Mathf.Abs(PosForCulc.y) >= _playerInputDeadzone))
                {
                    //現在の入力タイプを「フリック」とする
                    _inputType = InputType.Flick;
                }

                //操作タイプが「フリック」の時以下の処理を実行する
                if(_inputType == InputType.Flick)
                {
                    //生成処理を実行する
                    CreateAction(mousePosOnScreen);
                }
                            
            }               

            //クリック終了時に以下の処理を実行
            if(Input.GetKeyUp(KeyCode.Mouse0) && _touchNow)
            {
                //現在の入力タイプを初期化する
                _inputType = InputType.None;

                //プレイヤーの操作を終了する
                _touchNow = false;
            }
        }
        //実機での起動時に分岐(タッチ操作)
        else
        {
            //クリックの開始時に以下の処理を実行
            if(Input.touchCount > 0)
            {
                //プレイヤーの操作を受け付けている最中
                _touchNow = true;               

                //一つ目の指をトラッキング
                Touch touch = Input.GetTouch(0);

                //一つ目の指が画面に触れた時点で以下の処理を実行する
                if(touch.phase == TouchPhase.Began)
                {
                    //現在の入力タイプをタップとする
                    _inputType = InputType.Tap;

                    //タッチ開始時の座標を保持
                    _originalTouchPosition = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x,
                                                        touch.position.y));

                    //米を生成する(別ソースコードに転移)
                    //CreateRiceBaby(_originalTouchPosition);   

                    CreatePerticle(_originalTouchPosition);

                    //生成処理を実行する
                    CreateAction(_originalTouchPosition);

                }                

                //一つ目の指が動いている時に以下の処理を実行する
                if(touch.phase == TouchPhase.Moved)
                {
                    //一つ目の指の座標をスクリーン上に置き換えたものに加工
                    Vector2 touchPosOnScreen = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,
                                                                                        touch.position.y,10f));

                    //タッチ開始時の一つ目の指の座標と現在の一つ目の指の座標を引いて距離を求める
                    Vector2 PosForCulc = _originalTouchPosition - touchPosOnScreen;

                    //タッチ開始(プレイヤーの操作受け付け開始)からカーソルがデッドゾーン外に出た時点で「フリック」とする
                    if((Mathf.Abs(PosForCulc.x) >= _playerInputDeadzone || Mathf.Abs(PosForCulc.y) >= _playerInputDeadzone))
                    {
                        //現在の入力タイプを「フリック」とする
                        _inputType = InputType.Flick;
                    }

                    //生成処理を実行する
                    CreateAction(touchPosOnScreen);

                }
                
                if(touch.phase == TouchPhase.Ended)
                {
                    //現在の入力タイプを初期化する
                    _inputType = InputType.None;

                    //プレイヤーの操作を終了する
                    _touchNow = false;
                }                             
            }
        }
    }

    /// <summary>
    /// タップ時の処理を記述する関数
    /// </summary>
    private void CreateAction(Vector2 createPos)
    {
        //枠とりお米の座標を更新する
        //_riceBabyPhantom.transform.position = createPos;
        
        CapsuleDirection2D direction = new CapsuleDirection2D();

        //円形の光線を発射する(Circle型のRayCast)
        RaycastHit2D hit = 
        //Physics2D.CircleCast(center, _riceBabyRadius,Vector2.zero);
        Physics2D.CapsuleCast(createPos,new Vector2(0.5f,1f) * _riceBabyRadius,direction,0f,Vector2.zero,0f);

        //実行許可を持ちかつ光線がなんのオブジェクトも取得していない時に以下の処理を実行する
        if(_actionReady && hit.collider == null)
        {
            //他のソースコードに転移させた米を生成する処理を呼び出す
            RiceBabyCreateManager.Instance.CreateRiceBaby(createPos,0);
        }
        else
        {
            //オブジェクトを取得したときに流すログ
            //Debug.Log("超強力スーパーシュート！！");
        }

        //円形の光線を発射して当たった全てのオブジェクトの情報を取得する(ボツ改善案)
        // RaycastHit2D[] hits;
        // var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // //hits = Physics2D.RaycastAll(pos, Vector2.zero);
        // hits = Physics2D.CircleCastAll(pos, _riceBabyRadius,Vector2.zero);
        // Debug.DrawRay(pos, Vector2.zero, Color.green);
        // RaycastHit2D hit = hits.FirstOrDefault();

        //Array.Sort (hits,(a, b) => a.distance.CompareTo(b.distance));

        //実行許可を持ちかつ光線がなんのオブジェクトも取得していない時に以下の処理を実行する
        // if(_actionReady && hits.Length == 0)
        // {
        //     //他のソースコードに転移させた米を生成する処理を呼び出す
        //     RiceBabyCreateManager.Instance.CreateRiceBaby(createPos);
        // }
        // else
        // {
        //     //オブジェクトを取得したときに流すログ
        //     Debug.Log("超強力スーパーシュート！！");
        // }
    }
    

    // /// <summary>
    // /// 米の生成処理(別ソースコードに転移)
    // /// </summary>
    // /// <param name="pos">米を生成する座標</param>
    // private void CreateRiceBaby(Vector2 pos)
    // {
    //     //動作確認用
    //     //Debug.Log("ええやん！");

    //     //米の生成
    //     Instantiate(_riceBaby,pos,Quaternion.identity);
    // }

    // /// <summary>
    // /// プレイヤーの入力を受けるか弾くかを切り替える関数(設計に欠陥があったためボツ)
    // /// </summary>
    // public void InputSwitching()
    // {
    //     if(_actionReady)
    //     {
    //         _actionReady = false;
    //     }
    //     else
    //     {
    //         _actionReady = true;
    //     }
    // }

    /// <summary>
    /// 処理の実行許可証を有効にする
    /// </summary>
    public void PlayerActionOn()
    {
        _actionReady = true;
    }

    /// <summary>
    /// 処理の実行許可証を無効にする
    /// </summary>
    public void PlayerActionOff()
    {
        _actionReady = false;
    }

    public void CreatePerticle(Vector3 pos)
    {
        //クリック時の演出処理を実行
        Instantiate(_perticleObject, pos, quaternion.identity);
    }
}
