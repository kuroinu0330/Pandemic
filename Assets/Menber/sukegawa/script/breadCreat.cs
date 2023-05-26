using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breadCreat : MonoBehaviour
{
    [SerializeField]
    private GameObject Creatbread;
    public static breadCreat instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void bredCreator(Vector2 pos)
    {
        Instantiate(Creatbread,pos,Quaternion.identity);
    }
}
