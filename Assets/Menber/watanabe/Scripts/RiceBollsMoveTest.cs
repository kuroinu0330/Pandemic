using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiceBollsMoveTest : MonoBehaviour
{
    /*
    private Collider2D _collider01;
    private Collider2D _collider02;
    */

    public Text ScoreText;
    #region　米獲得
    [SerializeField]
    private int _level = 0; //レベル
    [SerializeField]
    private int _HighScore = 0; //米の獲得数
    #endregion
    #region 移動関係
    private GameObject _nearObj; //最も近いオブジェクト
    private float _serchTime;　
    [SerializeField]
    private float _speed;　//速度

    private bool _isArea;

    float m_radius;
    Vector3 center;

    #endregion
    // Start is called before the first frame updSate
    void Start()
    {
        _level = 0;
        //最も近いオブジェクトを取得
        _nearObj = serchTag(gameObject, "RiceBaby");
    }

    // Update is called once per frame
    /// <summary>
    /// 近くにいる米Objの方向に向いて追従する。
    /// </summary>
    void Update()
    {
        _serchTime += Time.deltaTime;
        if (_serchTime >= 0)
        {
            _nearObj = serchTag(gameObject, "RiceBaby");
            _serchTime = 0;
            //対象の位置の方向を向く
            //transform.LookAt(_nearObj.transform);
            if (_nearObj == null)
            {
                return;

            }
     
            Vector3 diff = (this.gameObject.transform.position - _nearObj.transform.position);

            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, -diff);

            CameraMoveController.Instance.CameraPositionUpdate();

            //自分自身の位置から相対的に移動する
            //transform.Translate(Vector3.forward * 0.1f);
            transform.position = Vector3.MoveTowards(
          transform.position,
          _nearObj.transform.position,
          _speed * Time.deltaTime);

        }
        ScoreText.text = "米獲得数: " + _HighScore.ToString();
    }
    public void blowoff()
    {
        Collider[] hitColliders = Physics.OverlapSphere(center,m_radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //komeが範囲に入ったら追いかける

        }
    }

    
    /// <summary>
    /// 一個米を取得するごとに10%速度が上昇
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RiceBaby"))
        {
            _level += 1;
            _HighScore += 1;
            other.gameObject.SetActive(false);
            #region レベル
            if (_level == 1)
            {
                //コルーチンStart
                StartCoroutine(CountCoroutine());
                Debug.Log("1レベルだよ");
                _speed = 110f;
                _nearObj = null;
            }
            if (_level == 2)
            {
                StartCoroutine(CountCoroutine());
                Debug.Log("2レベルだよ");
                _speed = 120f;
            }
            if (_level == 3)
            {
                StartCoroutine(CountCoroutine());
                Debug.Log("3レベルだよ");
                _speed = 130f;
            }
            if (_level == 4)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("4レベルだよ");
                _speed = 140f;
            }
            if (_level == 5)
            {
               
                StartCoroutine(CountCoroutine());
                Debug.Log("5レベルだよ");
                _speed = 150f;
            }
            if (_level == 6)
            {
             
                StartCoroutine(CountCoroutine());
                Debug.Log("6レベルだよ");
                _speed = 160f;
            }
            if (_level == 7)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("7レベルだよ");
                _speed = 170f;
            }
            if (_level == 8)
            {
              
                StartCoroutine(CountCoroutine());
                Debug.Log("8レベルだよ");
                _speed = 180f;
            }
            if (_level == 9)
            {
             
                StartCoroutine(CountCoroutine());
                Debug.Log("9レベルだよ");
                _speed = 190f;
            }
            if (_level == 10)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("10レベルだよ");
                _speed = 200f;
            }
            #endregion
        }
    }
    //一秒間米を獲得できなかったらスコアを0にする。
    bool _Clear =  false;
    /// <summary>
    /// 3秒間米を獲得できなかったらスコアと速度を初期化にする。
    /// </summary>
    /// <returns></returns>
    IEnumerator CountCoroutine()
    {
        _Clear = true;
        yield return null;
        float timer = 0.0f;
        _Clear = false;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= 2.0f)
            {
                _level = 0;
                _speed = 100f;
                Debug.Log("速度とレベルを初期に戻したぞい");
                _Clear = false;
                yield break;
            }
            if (_Clear)
            {
                _Clear = false;
                yield break;
            }
            yield return null;
        }
        /*yield return new WaitForSeconds(1.0f);
        _score = 0;
        _speed = 1.0f;
        Debug.Log("コルーチン開始");
        yield break;*/
    }
    public void ChallengeClear()
    {
        _Clear = true;
    }
 
    /// <summary>
    /// 米とおにぎりの距離を計算して近くにある米objに追従する
    /// </summary>
    /// <param name="nowObj">自分自身</param>
    /// <param name="tagName">米を探知</param>
    /// <returns></returns>
    #region 探知系
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        GameObject targetObj = null;
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            /*メモ
             * boolでfalseとtrueを使って画面内に要るときはtrueいないときはfalse
             *Vector2.DistanceではなくsqrMagnitudeを使った方が処理が軽い
             */
            //自身と取得したオブジェクトの距離を取得
             tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if (nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        return targetObj;
    }
    #endregion
}
