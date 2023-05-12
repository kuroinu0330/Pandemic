using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;


public class bread : MonoBehaviour
{
    Transform playerTr;//プレイヤーのTransform
    Transform riceTr;//米のTransform
    [SerializeField] float speed = 0.8f;//敵の動くスピード
     // Start is called before the first frame update
     
     //索敵範囲指定最初のプログラム
    public bool isSearching;
    public GameObject player;
    public GameObject rice;
    public static bread instance;

    void Awake()
    {
        if(instance ==null){
            instance = this;
        }
    }

    void Start()
    {
        // プレイヤーのTransformを取得（プレイヤーのタグをPlayerに設定必要）
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        riceTr = GameObject.FindGameObjectWithTag("rice").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーとの距離が0.1f未満になったらそれ以上実行しない
        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
            return;
        if (Vector2.Distance(transform.position, riceTr.position) < 0.1f)
            return;

        // プレイヤーに向けて進む(おにぎり)
        Transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTr.position.x, playerTr.position.y),
            speed * Time.deltaTime);
 　　　　// プレイヤーに向けて進む(米)
        Transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(riceTr.position.x, riceTr.position.y),
            speed * Time.deltaTime);

        //スピードの変化
        if (int score < 21)
        {
            speed = 0.8;
        }else if (score < 41)
        {
            speed = 1.0;
        }else if (score < 61)
        {
            speed = 1.2;
        }else if (score < 81)
        {
            speed = 1.5;
        }
    }
    //索敵範囲指定のプログラム開始地点
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearching = true;
            player = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isSearching = false;
            player = null;
        }
    }
    //索敵範囲指定のプログラム終了地点
}
