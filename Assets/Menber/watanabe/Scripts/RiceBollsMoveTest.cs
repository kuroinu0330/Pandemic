using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBollsMoveTest : MonoBehaviour
{
    #region　米獲得
    [SerializeField]
    private int _score = 0;

    #endregion
    #region 移動関係
    private GameObject _nearObj; //最も近いオブジェクト
    private float _serchTime;　　　　//
    [SerializeField]
    private float _speed;
    #endregion
    // Start is called before the first frame updSate
    void Start()
    {
        _score = 0;
        //最も近いオブジェクトを取得
        _nearObj = serchTag(gameObject, "kome");
    }

    // Update is called once per frame
    void Update()
    {
        _serchTime += Time.deltaTime;

        if (_serchTime >= 0)
        {
            _nearObj = serchTag(gameObject, "kome");
            _serchTime = 0;
            //対象の位置の方向を向く
            //transform.LookAt(_nearObj.transform);
            Vector3 diff = (this.gameObject.transform.position - _nearObj.transform.position);
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, -diff);
            //自分自身の位置から相対的に移動する
            //transform.Translate(Vector3.forward * 0.1f);
            transform.position = Vector2.MoveTowards(
          transform.position,
          _nearObj.transform.position,
          _speed * Time.deltaTime);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("kome"))
        {
            _score += 1;
            other.gameObject.SetActive(false);
            #region スコア
            if (_score == 1)
            {
               
                //コルーチンStart
                StartCoroutine(CountCoroutine());
                Debug.Log("1個目だよ");
                _speed = 1.1f;
            }
            if (_score == 2)
            {
               
                StartCoroutine(CountCoroutine());
                Debug.Log("2個目だよ");
                _speed = 1.2f;
            }
            if (_score == 3)
            {
               
                StartCoroutine(CountCoroutine());
                Debug.Log("3個目だよ");
                _speed = 1.3f;
            }
            if (_score == 4)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("4個目だよ");
                _speed = 1.4f;
            }
            if (_score == 5)
            {
               
                StartCoroutine(CountCoroutine());
                Debug.Log("5個目だよ");
                _speed = 1.5f;
            }
            if (_score == 6)
            {
             
                StartCoroutine(CountCoroutine());
                Debug.Log("6個目だよ");
                _speed = 1.6f;
            }
            if (_score == 7)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("7個目だよ");
                _speed = 1.7f;
            }
            if (_score == 8)
            {
              
                StartCoroutine(CountCoroutine());
                Debug.Log("8個目だよ");
                _speed = 1.8f;
            }
            if (_score == 9)
            {
             
                StartCoroutine(CountCoroutine());
                Debug.Log("9個目だよ");
                _speed = 1.9f;
            }
            if (_score == 10)
            {
                
                StartCoroutine(CountCoroutine());
                Debug.Log("10個目だよ");
                _speed = 2.0f;
            }
            #endregion
        }
    }
    //一秒間米を獲得できなかったらスコアを0にする。
    bool _Clear =  false;
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
                _score = 0;
                _speed = 1f;
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
 
    #region 探知系
    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        GameObject targetObj = null;
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector2.Distance(obs.transform.position, nowObj.transform.position);

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
