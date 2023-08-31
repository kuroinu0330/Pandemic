using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle1 : MonoBehaviour
{
    private GameObject nearObj1,nearObj2;
    private bool isHit;
    private void Start()
    {
        //Tag�̎擾
        nearObj1 = GameObject.FindWithTag( "Player");
        nearObj2 = GameObject.FindWithTag("RiceBaby");
    }

   
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Kyori.instance.kyori() ;
        isHit = true ;
    }

}
