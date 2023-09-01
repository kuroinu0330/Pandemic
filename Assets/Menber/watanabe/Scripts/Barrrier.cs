using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrrier : MonoBehaviour
{
    [SerializeField]
    private GameObject _destroyObj;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bread"))
        {
            Debug.Log("bibinba");
            BreadManager.Instance.RemoveListOOOOOOOOOOOOOOORU(other.gameObject);
            Destroy(other.gameObject);
             //other.gameObject.SetActive(false);
        }
    }
}
