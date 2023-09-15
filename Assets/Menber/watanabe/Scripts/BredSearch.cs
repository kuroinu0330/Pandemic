using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BredSearch : MonoBehaviour
{
    public RiceBollsMoveTest riceBollsMoveTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //riceBollsMoveTest.BreadSearch();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RiceBaby"))
        {
            //riceBollsMoveTest.BreadSearch();
        }
    }
}
