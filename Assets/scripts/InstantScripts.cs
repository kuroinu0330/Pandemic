using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantScripts : MonoBehaviour
{
    void InstantActionOn()
    {
        PlayerInputManagement.Instance.PlayerActionOn();
        TileScrollManager.Instance.RiceBabyRandomGenerationStart();
        TileScrollManager.Instance.GenerationBreadInit();
        StartCoroutine(ItemGenarationManager.instance.timerCountUpper());
    }

    void InstantActionOn2()
    {
        //bread.instance.ActionOnTheWay();
        BreadManager.Instance.BreadsActionOn();
    }
}
