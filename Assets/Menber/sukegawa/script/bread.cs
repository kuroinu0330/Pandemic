using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class bread : MonoBehaviour
{
    //Transform playerTr;//プレイヤーのTransform
    //Transform riceTr;//米のTransform
    //[SerializeField] private bread Bread;
    // Start is called before the first frame update
    //接触用の当たり判定
    [SerializeField] private BoxCollider2D Collider;
    //探知用の当たり判定
    [SerializeField] private CircleCollider2D Trigger;
    //探知したオブジェクトをまとめるリスト
    [SerializeField] private List<GameObject> TargetObject;
    //ターゲットオブジェクト
    [SerializeField] private GameObject Target;
    //移動速度
    [SerializeField]private float MoveSpeed = 0.8f;
    //移動速度倍率
    [SerializeField] private float MoveSpeedRatio = 1f;
   // public char[] charArray;
    //public string str;
    //索敵範囲指定最初のプログラム
    //public bool isSearching;
    public GameObject player;
    public GameObject rice;
    public static bread instance;
    private Animator _aniem;
    //int judge = 1;
    //instance化の設定
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // プレイヤーのTransformを取得（プレイヤーのタグをPlayerに設定必要）
        // playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        //riceTr = GameObject.FindGameObjectWithTag("RiceBaby").transform;
        //StartCoroutine ("firststop");
        //Bread = GetComponent<bread>();
        _aniem = GetComponent<Animator>();
    }
    /// <summary>
    /// 探知成功時の処理
    /// </summary>
    /// <param name ="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        TargetObject.Add(other.gameObject);
        TargetSelectEXE();
    }
    /*
    void SetState(breadCreat Creatbread)
    {

    }
   */
    // Update is called once per frame
    /// <summary>
    /// 接触成功時の処理
    /// </summary>
    /// <param name="collisionInfo"></param>
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        switch (collisionInfo.gameObject.tag)
        {
            case "Player":

                //プレイヤー接触時の処理
                //配列から対象のオブジェクトwp削除
                TargetObject.Remove(collisionInfo.gameObject);
                //シーン上から対象のオブジェクトを削除
                Target = null;
                animetionfalse();
                //ターゲットを初期化
                break;
            
            case "RiceBaby":
                //米獲得時の処理
                //配列から対象のオブジェクトを削除
                TargetObject.Remove(collisionInfo.gameObject);
                //シーン上から対象のオブジェクトを削除
                Target = null;
                //米を獲得した時の処理
                collisionInfo.gameObject.SetActive(false);
                //ターゲットを初期化
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
        for (int i = 0; i < TargetObject.Count; i++)
        {
            //探知先のオブジェクトまでの距離を算出
            float Distance = Vector3.Distance(this.gameObject.transform.position, TargetObject[i].gameObject.transform.position);

            //ターゲットが存在しない場合以下の処理を実行する   
            if (Target == null)
            {
                //現在の探知済みのオブジェクトを対象にする
                Target = TargetObject[i];
            }
            //ターゲットが存在する場合以下の処理を実行する
            else
            {
                //ターゲットのオブジェクトまでの距離を算出
                float Dis = Vector3.Distance(this.gameObject.transform.position, Target.gameObject.transform.position);

                //ターゲットまでの距離が探知済みのオブジェクトとの距離よりも近いもしくはプレイヤーだった場合更新する
                if (TargetObject[i].tag == "Player")
                {
                    //移動先の更新処理
                    Target = TargetObject[i];
                }
                else if (Dis > Distance || Target.tag != "Player")
                {
                    //移動先の更新処理
                    Target = TargetObject[i];
                }
            }
        }
        
    }
    void Update()
    {
        if (Target != null)
        {
            //移動処理
            transform.position= Vector3.MoveTowards(this.gameObject.transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
            animetiontrue();
        }



        /*
        judge = Circle1.instance.Judge();
        switch (judge)
        {

            case 1:
                // プレイヤーに向けて進む(おにぎり)
                this.transform.position = Vector2.MoveTowards(
                                this.transform.position,
                              new Vector2(playerTr.position.x, playerTr.position.y),
                              MoveSpeed * Time.deltaTime);
                break;
            case 2:
                // プレイヤーに向けて進む(米)
                this.transform.position = Vector2.MoveTowards(
                                this.transform.position,
                                new Vector2(riceTr.position.x, riceTr.position.y),
                                MoveSpeed * Time.deltaTime);
        break;

        }
        
            int score = 0;
            //スピードの変化
            if (score < 21f)
            {
                speed = 0.8f;
            }
            else if (score < 41f)
            {
                speed = 1.0f;
            }
            else if (score < 61f)
            {
                speed = 1.2f;
            }
            else if (score < 81f)
            {
                speed = 1.5f;
            }
        
        
        // プレイヤーとの距離が0.1f未満になったらそれ以上実行しない

        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
         return;

        */
    }
    private void animetiontrue()
    {
        _aniem.SetBool("bread Bool", true);
    }
    private void animetionfalse() 
    {
        _aniem.SetBool("bread Bool", false);
    }
    //索敵範囲指定のプログラム開始地点
   
    /* void OnTriggerEnter2D(Collider2D col)
     {
        if (col.gameObject.tag == "Player")
        {

        }

        if(col.gameObject.tag == "RiceBaby")
        {
            firststop();
        }
     }*/

    /*private IEnumerator firststop()
    {
        //1秒停止
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.0f);
        //再開
        Time.timeScale = 1;
    

        
            while (true)
            {*/
                // プレイヤーに向けて進む(おにぎり)
                /*this.transform.position = Vector2.MoveTowards(
                  this.transform.position,
                  new Vector2(playerTr.position.x, playerTr.position.y),
                  speed * Time.deltaTime);
                */
                // プレイヤーに向けて進む(米)
                /*this.transform.position = Vector2.MoveTowards(
                    this.transform.position,
                    new Vector2(riceTr.position.x, riceTr.position.y),
                    speed * Time.deltaTime);*/
                /*
                int score = 0;

                //スピードの変化
                if (score < 21f)
                {
                    speed = 0.8f;
                }
                else if (score < 41f)
                {
                    speed = 1.0f;
                }
                else if (score < 61f)
                {
                    speed = 1.2f;
                }
                else if (score < 81f)
                {
                    speed = 1.5f;
                }*/
               /* yield return null;
            }
            //yield break;
        }*/
        //索敵範囲指定のプログラム終了地点
        //public void breadIn(){
        //    //Instantiate(bread);
        //}
    
}

