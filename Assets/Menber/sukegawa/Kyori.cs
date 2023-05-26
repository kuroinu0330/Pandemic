using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Kyori : MonoBehaviour
{
    Transform playerTr;//プレイヤーのTransform
    Transform riceTr;//米のTransform
    [SerializeField] float speed = 0.8f;//敵の動くスピード
    public static Kyori instance;
    //instance化の設定
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //索敵範囲指定最初のプログラム
    public bool isSearching;
    public GameObject player;
    public GameObject rice;
    public void kyori()
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            int score = 0;

        
            while (true)
            {
                if (col.gameObject.tag == "Player")
                {
                    // プレイヤーに向けて進む(おにぎり)
                    this.transform.position = Vector2.MoveTowards(
                        this.transform.position,
                      new Vector2(playerTr.position.x, playerTr.position.y),
                      speed * Time.deltaTime);
                }
                else if (col.gameObject.tag == "rice")
                {
                    // プレイヤーに向けて進む(米)
                    this.transform.position = Vector2.MoveTowards(
                        this.transform.position,
                        new Vector2(riceTr.position.x, riceTr.position.y),
                        speed * Time.deltaTime);
                }
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
            }
        }
   }
}*/
