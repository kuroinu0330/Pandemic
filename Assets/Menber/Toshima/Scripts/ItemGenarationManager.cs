using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenarationManager : MonoBehaviour
{
    [SerializeField] 
    private List<GameObject> _itemPrefabs;

    [SerializeField] 
    private GameObject _itemHangar;

    [SerializeField] 
    private GameObject _pulseShield;

    [SerializeField]
    private int[] _genarationTemporaryCount = { 0, 0, 0, 0 };
    
    public enum ItemList
    {
        None,
        Acceleration,
        invincible,
        infinite,
        Double
    }
    
    public static ItemGenarationManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GenarationItem(Vector2 pos,ItemList Item = ItemList.None)
    {
        GameObject gameObject;

        if (Item == ItemList.None)
        {
            Item = LotteryItemType();
        }

        switch (Item)
        {
            case ItemList.Acceleration:
                gameObject = _itemPrefabs[0];
                StartCoroutine(CountUpDown(0));
                break;
            case ItemList.invincible:
                gameObject = _itemPrefabs[1];
                gameObject.GetComponent<InvincibleItems>().SetBarria(_pulseShield);
                StartCoroutine(CountUpDown(1));
                break;
            case ItemList.infinite:
                gameObject = _itemPrefabs[2]; 
                StartCoroutine(CountUpDown(2));
                break;
            case ItemList.Double:
                gameObject = _itemPrefabs[3];
               StartCoroutine(CountUpDown(3));
                break;
            default:
                gameObject = null;
                break;
        }

        if (gameObject != null)
        {
            var obj = Instantiate(gameObject, pos, Quaternion.identity);

            obj.transform.parent = _itemHangar.transform;
        }
    }

    private ItemList LotteryItemType()
    {
        ItemList Item = ItemList.None;

        int SerectNum = 0;

        int RandNum = 0;

        int[] ItemMixingRatio = {3,3,3,3};

        for (int i = 0; i < _genarationTemporaryCount.Length; i++)
        {
            ItemMixingRatio[i] -= _genarationTemporaryCount[i];
            SerectNum += ItemMixingRatio[i];    
        }

        RandNum = Random.Range(0,SerectNum);

        if (RandNum <= ItemMixingRatio[0] && ItemMixingRatio[0] != 0)
        {
            Item = ItemList.Acceleration;
        }
        else if (RandNum <= ItemMixingRatio[0] + ItemMixingRatio[1] && ItemMixingRatio[1] != 0)
        {
            Item = ItemList.invincible;
        }
        else if (RandNum <= ItemMixingRatio[0] + ItemMixingRatio[1] + ItemMixingRatio[2] && ItemMixingRatio[2] != 0)
        {
            Item = ItemList.infinite;
        }
        else if (RandNum <= ItemMixingRatio[0] + ItemMixingRatio[1] + ItemMixingRatio[2] + ItemMixingRatio[3] && ItemMixingRatio[3] != 0)
        {
            Item = ItemList.Double;
        }
        else
        {
            Item = ItemList.None;
            Debug.Log("ロジックエラー");
        }
        
        return Item;
    }

    private IEnumerator CountUpDown(int num)
    {
        //Debug.Log("カウントスタート");
        _genarationTemporaryCount[num] += 1;

        float TimeLimit = 120f;

        float timer = 0.0f;

        while (TimeLimit >= timer)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            yield return null;
        }
        Debug.Log("カウントエンド");
        _genarationTemporaryCount[num] -= 1;
        
        //Debug.Log("カウントエンド");

        yield break;
    }
}
