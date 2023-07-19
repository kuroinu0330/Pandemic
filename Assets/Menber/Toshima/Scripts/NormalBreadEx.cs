using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalBreadEx : MonoBehaviour
{
    //探知したオブジェクトとプレイヤーをまとめておくリスト
    [SerializeField]
    private List<GameObject> _targerObject;

    //自身の当たり判定を保持する変数
    [SerializeField] 
    private CircleCollider2D collider2D;
    
    //デフォルトの移動速度
    [SerializeField]
    private float _moveSpeed;

    //加速倍率(合計値確認用)
    [SerializeField]
    private float _moveSpeedResio;
    
    //デフォルトの探知範囲
    [SerializeField]
    private float _detectionRange;
    
    //探知範囲の拡大率(合計値確認用)
    [SerializeField]
    private float _detectionRangeScale;
    
    //行動可能フラグ
    [SerializeField]
    private bool _actionOn = true;

    [SerializeField]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BreadMove());
    }

    private void FixedUpdate()
    {
        if (_targerObject.Count >= 2)
        {
            //今世紀最大の気持ち悪コード
            _targerObject.Sort((a, b) => MathF.Abs(Vector3.Distance(this.transform.position, a.transform.position))
                .CompareTo(MathF.Abs(Vector3.Distance(this.transform.position, b.transform.position)))); 
        }
    }

    //移動用のコルーチン
    private IEnumerator BreadMove()
    {
        //初生成時一秒待機する
        yield return new WaitForSeconds(1f);

        //本移動処理
        while (true)
        {
            if (_actionOn && BreadManager.Instance.GetActionBool() && _targerObject.Count != 0)
            {
                //アニメーションスタート
                BreadAnimetion();
                
                //最寄りのオブジェクトに向けた1フレーム先の座標で更新
                this.transform.position = Vector2.MoveTowards(this.transform.position,
                    _targerObject[0].transform.position,
                    _moveSpeed * _moveSpeedResio * Time.deltaTime);
            }
            else
            {
                //アニメーション停止
                anim.SetBool("BreadBool",false);
            }
            
            yield return null;
        }
    }

    private IEnumerator StopIt()
    {
        ActionOff();
        yield return new WaitForSeconds(1f);
        ActionOn();
        yield break;
    }

    public void BreadAnimetion()
    {
        anim.SetBool("BreadBool",true);
    }
    
    /// <summary>
    /// 接触判定
    /// </summary>
    /// <param name="other">接触先のオブジェクト</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        //接触したオブジェクトのタグによって処理を分岐する
        switch (other.gameObject.tag)
        {
            //プレイヤータグの場合以下の処理を実行する
            case "Player":
                BreadManager.Instance.TouchPlayer();
                break;
            //米粒タグの場合以下の処理を実行する
            case "RiceBaby":
                StartCoroutine(StopIt());
                other.gameObject.SetActive(false);
                Vector3 vec = new Vector3(100f, 0f, 0f);
                BreadManager.Instance.GenerateBread(this.transform.position + vec);
                this.transform.position -= vec;
                break;
        }
    }

    /// <summary>
    /// 米粒を発見した時に呼ばれる関数
    /// </summary>
    /// <param name="obj"></param>
    public void ListAdd(GameObject obj)
    {
        _targerObject.Add(obj);
    }

    /// <summary>
    /// 米粒を見失った時に呼ばれる関数
    /// </summary>
    /// <param name="obj"></param>
    public void ListRemove(GameObject obj)
    {
        _targerObject.Remove(obj);
    }

    /// <summary>
    /// プレイヤーのtransformをマネージャー経由でセットする関数(探すより絶対軽い)
    /// </summary>
    /// <param name="transform"></param>
    public void SetPlayerPosition(GameObject player)
    {
       _targerObject.Add(player);
    }

    /// <summary>
    /// パンの加速倍率を変更する関数
    /// </summary>
    /// <param name="rank"></param>
    public void MoveSpeedUpRank(int rank)
    {
        _moveSpeedResio = 1f + 0.25f * rank;
    }

    /// <summary>
    /// パンの探知範囲拡大率を変更する関数
    /// </summary>
    /// <param name="rank"></param>
    public void DetectionRangeUpScale(int rank)
    {
        _detectionRangeScale = 1f + 0.05f * rank;

        collider2D.radius = _detectionRange * _detectionRangeScale;
    }

    public void ActionOn()
    {
        _actionOn = true;
    }

    public void ActionOff()
    {
        _actionOn = false;
    }
}
