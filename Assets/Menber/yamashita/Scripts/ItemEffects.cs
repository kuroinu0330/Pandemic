using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject accelEffect;
    [SerializeField]
    private GameObject doubleEffect;
    [SerializeField]
    private GameObject infinityEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Accel"))
        {
            accelEffect.SetActive(true);
        }
        
        if (col.gameObject.CompareTag("Double"))
        {
            doubleEffect.SetActive(true);
        }

        if (col.gameObject.CompareTag("Infinity"))
        {
            infinityEffect.SetActive(true);
        }
    }
}
