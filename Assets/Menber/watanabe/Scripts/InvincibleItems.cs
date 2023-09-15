using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItems : MonoBehaviour
{
    [SerializeField]
    private GameObject _Barria;
    //���G����
    [SerializeField]
    private float _InvincibleTime = 0;

    public static InvincibleItems _Item;

    public void SetBarria(GameObject obj)
    {
        _Barria = obj;
    }

    public IEnumerator DamageLiberion()
    {
        Debug.Log("iiiiiii");
        float _Time = 0.0f;
        yield return null;
        while (true)
        {
            Debug.Log("kkkkkk");
            _Time += Time.deltaTime;
            if (_InvincibleTime >= _Time)
            {
                Debug.Log("aaaaa");
                _Barria.SetActive(true);
            }
            else
            {
                _Barria.SetActive(false);

            }
            yield return null;
        }
    }
    private void Awake()
    {
        _Item = this.gameObject.GetComponent<InvincibleItems>();
    }
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Invincble"))
        {
            StartCoroutine(InvinciblieTime());
        }
    }*/

}
