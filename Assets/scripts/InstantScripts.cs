using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantScripts : MonoBehaviour
{
    void InstantActionOn()
    {
        PlayerInputManagement.Instance.PlayerActionOn();
        TileScrollManager.Instance.RiceBabyRandomGenerationStart();
    }

    void InstantActionOn2()
    {
        //bread.instance.ActionOnTheWay();
        BreadManager.Instance.BreadsActionOn();
    }
}
