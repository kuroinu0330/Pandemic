using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibleBreak : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Bread"))
        {
            Debug.Log("hoge");
            Destroy(other.gameObject);
        }
    }
}
