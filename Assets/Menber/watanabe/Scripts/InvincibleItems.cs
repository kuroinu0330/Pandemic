using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleItems : MonoBehaviour
{
    [SerializeField]
    private GameObject _Barria;
    //–³“GŽžŠÔ
    [SerializeField]
    private float _InvincibleTime = 0;

    public static InvincibleItems _Item;
   
    public IEnumerator DamageLiberion()
    {
        float _Time = 0.0f;
        yield return null;
        while (true)
        {
            _Time += Time.deltaTime;
            if (_InvincibleTime >= _Time)
            {
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
}
