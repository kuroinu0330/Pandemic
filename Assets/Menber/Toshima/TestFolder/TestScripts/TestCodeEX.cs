using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCodeEX : MonoBehaviour
{
    //接触用の当たり判定
    [SerializeField]
    private BoxCollider2D Collider;

    //探知用の当たり判定
    [SerializeField]
    private CircleCollider2D Trigger;

    //探知したオブジェクトをまとめるリスト
    [SerializeField]
    private List<GameObject> TargetObjects;

    //ターゲットオブジェクト
    [SerializeField]
    private GameObject Target;

    //移動速度
    [SerializeField]
    private float MoveSpeed = 1f;

    //移動速度倍率
    // [SerializeField]
    // private float MoveSpeedRatio = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 探知成功時の処理
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //配列に探知したオブジェクトを追記
        TargetObjects.Add(other.gameObject);

        //ターゲットの設定をする
        TargetSelectEXE();
    }

    /// <summary>
    /// 接触成功時の処理
    /// </summary>
    /// <param name="collisionInfo"></param>
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Player":

            //プレイヤー接触時の処理
            //配列から対象のオブジェクトを削除
            TargetObjects.Remove(collisionInfo.gameObject);
            //シーン上から対象のオブジェクトを削除
            Destroy(collisionInfo.gameObject);
            //ターゲットを初期化
            Target = null;

            break;
            case "RiceBaby":

            //米獲得時の処理
            //配列から対象のオブジェクトを削除
            TargetObjects.Remove(collisionInfo.gameObject);
            //シーン上から対象のオブジェクトを削除
            Destroy(collisionInfo.gameObject);
            //ターゲットを初期化
            Target = null;

            break;
            default:
            //不具合防止

            break;
        }

        //ターゲットの設定をする
        TargetSelectEXE();
    }

    /// <summary>
    /// 探知されたオブジェクトと自身の位置からターゲットを決める
    /// </summary>
    void TargetSelectEXE()
    {
        //探知したオブジェクトの数だけ以下の処理を実行する
        for(int i = 0; i < TargetObjects.Count;i++)
        {
            //探知先のオブジェクトまでの距離を算出
            float Distance = Vector3.Distance(this.gameObject.transform.position,TargetObjects[i].gameObject.transform.position);
            
            //ターゲットが存在しない場合以下の処理を実行する
            if(Target == null)
            {
                //現在の探知済みのオブジェクトを対象にする
                Target = TargetObjects[i];
            }
            //ターゲットが存在する場合以下の処理を実行する
            else
            {
                //ターゲットのオブジェクトまでの距離を算出
                float Dis = Vector3.Distance(this.gameObject.transform.position,Target.gameObject.transform.position);
                
                //ターゲットまでの距離が探知済みのオブジェクトとの距離よりも近いもしくはプレイヤーだった場合更新する
                if(TargetObjects[i].tag == "Player")
                {
                    //移動先の更新処理
                    Target = TargetObjects[i];
                }
                else if(Dis > Distance || Target.tag != "Player")
                {
                    //移動先の更新処理
                    Target = TargetObjects[i];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {

            //移動処理
            transform.position = Vector3.MoveTowards(this.gameObject.transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);

        }
    }
}
