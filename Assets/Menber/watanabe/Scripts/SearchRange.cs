using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SearchRange : MonoBehaviour
{
    private CircleCollider2D _collider;
    [SerializeField]
    private int _sizeCount;

    public int size;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {   

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RiceBaby"))
        {

        }
    }


}
