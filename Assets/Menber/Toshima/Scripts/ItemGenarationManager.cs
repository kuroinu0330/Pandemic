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

    [SerializeField]
    private InfinitStaminaUI _initStaminaUI;

    [SerializeField]
    private DualSabotUI _dualSabotUI;

    [SerializeField] private float timer;
    [SerializeField] private int count;
    
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

        timer = 40.0f;
        count = 4;
    }

    public IEnumerator timerCountUpper()
    {
        while (timer <= 40)
        {
            if (timer >= 10 && count == 4)
            {
                count--;
            }
            else if (timer >= 20 && count == 3)
            {
                count--;
            }
            else if (timer >= 30 && count == 2)
            {
                count--;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        count--;
        yield break;
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
                Debug.Log("加速アイテム");
                StartCoroutine(CountUpDown(0));
                break;
            case ItemList.invincible:
                gameObject = _itemPrefabs[1];
                Debug.Log("無敵アイテム");
                gameObject.GetComponent<InvincibleItems>().SetBarria(_pulseShield);
                StartCoroutine(CountUpDown(1));
                break;
            case ItemList.infinite:
                gameObject = _itemPrefabs[2];
                gameObject.GetComponent<InfiniteStaminaRiceBaby>().infinitStaminaUI = _initStaminaUI;
                Debug.Log("無限アイテム");
                StartCoroutine(CountUpDown(2));
                break;
            case ItemList.Double:
                gameObject = _itemPrefabs[3];
                gameObject.GetComponent<DualSabotRiceBaby>().dualSabotUI = _dualSabotUI;
                Debug.Log("増加アイテム");
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

        for (int i = 0; i < _genarationTemporaryCount.Length - count; i++)
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

        if (count != 4)
        {
            return Item;
        }

        return ItemList.None;
    }

    private IEnumerator CountUpDown(int num)
    {
        //Debug.Log("カウントスタート");
        _genarationTemporaryCount[num] += 1;

        float TimeLimit = 20f;

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
