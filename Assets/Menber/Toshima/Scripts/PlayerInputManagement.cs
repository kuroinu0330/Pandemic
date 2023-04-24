using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerInputManagement : MonoBehaviour
{
    //入力判定用のデッドゾーン
    [SerializeField]
    private float _playerInputDeadzone = 0f;

    [SerializeField]
    private GameObject _riceBaby;

    //プレイヤーがタッチを始めた最初の座標
    [SerializeField]
    private Vector2 _originalTouchPosition = Vector2.zero;

    //プレヤーの操作が「タッチ」か「フリック」かを保持する
    [SerializeField]
    private InputType _inputType = InputType.None;

    //現在プレイヤーの入力を受け付けるかの変数
    [SerializeField]
    private bool _inputReady = false;

    //現在入力中化を判断する変数
    [SerializeField]
    private bool _touchNow = false;

    //入力方法の一覧
    private enum InputType
    {
        None,
        Tap,
        Flick
    }

    void Start() 
    {
        //デバック用の強制許可処理
        InputSwitching();
    }

    void Update()
    {
        //プレイヤーの操作を受け付ける場合に実行
        if(_inputReady)
        {
            InputTypejudgement();
        }
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

                //タップ時の処理を実行する

                //米を生成する
                CreateRiceBaby(_originalTouchPosition);                
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

                //フリック時の処理を実行する
                
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

                    //米を生成する
                    CreateRiceBaby(_originalTouchPosition);   

                    //タップ時の処理を実行する


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

                    //フリック時の処理を実行する


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
    /// 米の生成処理
    /// </summary>
    /// <param name="pos">米を生成する座標</param>
    private void CreateRiceBaby(Vector2 pos)
    {
        //動作確認用
        //Debug.Log("ええやん！");

        //米の生成
        Instantiate(_riceBaby,pos,Quaternion.identity);
    }

    /// <summary>
    /// プレイヤーの入力を受けるか弾くかを切り替える関数
    /// </summary>
    public void InputSwitching()
    {
        if(_inputReady)
        {
            _inputReady = false;
        }
        else
        {
            _inputReady = true;
        }
    }
}
