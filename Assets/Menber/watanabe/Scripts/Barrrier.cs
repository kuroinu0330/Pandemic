using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrrier : MonoBehaviour
{
    [SerializeField]
    private GameObject _destroyObj;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bread"))
        {
            Debug.Log("atata");
            //Destroy(other.gameObject);
             other.gameObject.SetActive(false);
        }
    }
}
