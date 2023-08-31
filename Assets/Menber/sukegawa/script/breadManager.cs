using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class breadManager : MonoBehaviour
{
    //
    public Transform target;
    public GameObject bread;
    void Place()//
    {
        Vector3 pos1 = target.transform.position;
        pos1.x = target.position.x;
        pos1.y = target.position.y;
        transform.position = pos1;

    }
    [SerializeField]
    int score = 0;
    public float moveSpeed = 0.8f;
    public void Speed()
    {
        if(score > 20)
        {
            moveSpeed = 1.0f;
        }else if (score > 40)
        {
            moveSpeed = 1.2f;
        }
        else if (score > 60)
        {
            moveSpeed = 1.5f;
        }
        else
        {
            moveSpeed = 0.8f;
        }
    }

    public void Multiply()//増殖数のスクリプトの挿入をお願いします
    {
        if (score < 20)
        {

        }else if(score < 40){

        }
        else if(score < 60)
        {

        }
    }

    GameObject[] tagObjects;//
    public void RiceBabyPlace()
    {
        tagObjects = GameObject.FindGameObjectsWithTag("RiceBaby");
        if (tagObjects.Length == 0)
        {
            SceneManager.LoadScene("BreadTest");//ゲーム画面のシーンに名前の変更お願いします。
        }
        float range;
        float rangedefault=30f;//デフォルト索敵範囲
        if(tagObjects.Length == 1)
        {
            //range = 31.5f;
            range = rangedefault * 1.05f;
        }else if(tagObjects.Length==2){
            //range =33;
            range = rangedefault * 1.10f;
        }
        else if (tagObjects.Length == 3)
        {
            //range =34.5f;
            range = rangedefault * 1.15f;
        }
        else if (tagObjects.Length == 4)
        {
            //range =36f;
            range = rangedefault * 1.20f;
        }
        else if (tagObjects.Length == 5)
        {
            //range =37.5f;
            range = rangedefault * 1.25f;
        }
        else if (tagObjects.Length ==6 )
        {
            //range =39f;
            range = rangedefault * 1.30f;
        }
        else if (tagObjects.Length == 7)
        {
            //range =40.5f;
            range = rangedefault * 1.35f;
        }
        else if (tagObjects.Length == 8)
        {
            //range =42f;
            range = rangedefault * 1.40f;
        }
        else if (tagObjects.Length == 9)
        {
            //range =43.5f;
            range = rangedefault * 1.45f;
        }
        else if (tagObjects.Length == 10)
        {
            //range =45;
            range = rangedefault * 1.50f;
        }

    }
}

