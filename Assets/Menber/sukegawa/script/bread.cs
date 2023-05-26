using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
<<<<<<< HEAD
using UnityEngine;

/*public class bread : MonoBehaviour
{
    Transform playerTr;//�v���C���[��Transform
    [SerializeField] float speed = 0.8;//�G�̓����X�s�[�h
     // Start is called before the first frame update
    public bool isSearching;
    public GameObject player;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
            return;

        Transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTr.position.x, playerTr.position.y),
            speed * Time.deltaTime);

        if (score < 21)
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
}*/
=======
using Unity.VisualScripting;
using UnityEngine;

public class bread : MonoBehaviour
{
    Transform playerTr;//プレイヤーのTransform
    Transform riceTr;//米のTransform
    [SerializeField] float speed = 0.8f;//敵の動くスピード


    [SerializeField] private bread Bread;
   // [SerializeField] private Circle1 circle1;
    //[SerializeField] private Circle2 circle2;
    //[SerializeField] private Circle3 circle3;
    // Start is called before the first frame update

    public char[] charArray;
    public string str;
    //索敵範囲指定最初のプログラム
    public bool isSearching;
    public GameObject player;
    public GameObject rice;
    public static bread instance;
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
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        //riceTr = GameObject.FindGameObjectWithTag("RiceBaby").transform;
        StartCoroutine ("firststop");
        
        //Bread = GetComponent<bread>();
    }
    void SetState(breadCreat Creatbread)
    {

    }
   
    // Update is called once per frame
    void Update()
    {
        // プレイヤーとの距離が0.1f未満になったらそれ以上実行しない

        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
            return;


    }
    //索敵範囲指定のプログラム開始地点
   
     void OnTriggerEnter2D(Collider2D col)
     {
        if (col.gameObject.tag == "Player")
        {

        }

        if(col.gameObject.tag == "RiceBaby")
        {
            firststop();
        }
     }

    private IEnumerator firststop()
    {
        //1秒停止
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.0f);
        //再開
        Time.timeScale = 1;
    

        
            while (true)
            {
                // プレイヤーに向けて進む(おにぎり)
                this.transform.position = Vector2.MoveTowards(
                  this.transform.position,
                  new Vector2(playerTr.position.x, playerTr.position.y),
                  speed * Time.deltaTime);

                // プレイヤーに向けて進む(米)
                /*this.transform.position = Vector2.MoveTowards(
                    this.transform.position,
                    new Vector2(riceTr.position.x, riceTr.position.y),
                    speed * Time.deltaTime);*/

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
                yield return null;
            }
            //yield break;
        }
        //索敵範囲指定のプログラム終了地点
        //public void breadIn(){
        //    //Instantiate(bread);
        //}
    
   }

>>>>>>> origin/sukegawa
