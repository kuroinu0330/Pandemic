/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SearchRange : MonoBehaviour
{
    private CircleCollider2D _collider;
    [SerializeField]
    private int _sizeCount;

    RiceBollsMoveTest riceBollsMoveTest;
    public int size;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {   

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("RiceBaby"))
        {
            Debug.Log("aaaa");
            riceBollsMoveTest.RiceBollsMove();
        }
    }
    
}
*/