using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class bread : MonoBehaviour
{
    Transform playerTr;//プレイヤーのTransform
    [SerializeField] float speed = 0.8;//敵の動くスピード
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
}
