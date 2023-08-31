using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField]
    private bool[] itemFlags = new bool[6];
    public Item item;// éÌóﬁ
    public String infomation;// èÓïÒ
    public enum Item
    {
        InvincibleItem,
        AccelarationItem,
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (item)
        {
            case Item.InvincibleItem:
                break;
            case Item.AccelarationItem:
                break;
        }

    }
}
