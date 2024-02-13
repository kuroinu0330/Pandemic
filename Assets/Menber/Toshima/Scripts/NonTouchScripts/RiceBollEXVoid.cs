using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBollEXVoid : MonoBehaviour
{
   [SerializeField]
   private RiceBollsMoveTest scripts;

   public static RiceBollEXVoid instance;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
   }

   public IEnumerator SwitchActive()
   {
      scripts.AreaFlashEXE(true);
      float timelimit = 0.05f;

      float timer = 0.0f;
      while (scripts.TargetIsNull())
      {
         //Debug.Log("あの山の頂へ！！");
         if (this.gameObject.activeSelf && timelimit <= timer)
         {
            this.gameObject.SetActive(false);
            timer = 0.0f;
         }
         else if(!this.gameObject.activeSelf && timelimit <= timer)
         {
            this.gameObject.SetActive(true);
            timer = 0.0f;
         }

         timer += Time.deltaTime;
         yield return null;
      }
      scripts.AreaFlashEXE(false);
      //Debug.Log("スペース");
      yield break;
   }
   
   /// <summary>
   /// 検出判定
   /// </summary>
   /// <param name="other"></param>
   private void OnTriggerEnter2D(Collider2D other)
   {
      //米粒タグの場合以下の処理を実行する
      if (JudgeThis(other.gameObject))
      {
         //米粒オブジェクトを配列に叩き込む
         scripts.GameObjectAdd(other.gameObject);
         //Debug.Log("米粒検知");
      }
   }

   /// <summary>
   /// 喪失判定
   /// </summary>
   /// <param name="other"></param>
   private void OnTriggerExit2D(Collider2D other)
   {
      //米粒タグの場合以下の処理を実行する
      if (JudgeThis(other.gameObject))
      {
         //米粒オブジェクトを配列から叩き出す
         scripts.GameObjectRemove(other.gameObject);
         Debug.Log("米粒損失");
      }
   }

   private bool JudgeThis(GameObject other)
   {
      if (other.CompareTag("RiceBaby") || other.CompareTag("AccelerationItem") ||
          other.CompareTag("Invincble") || other.CompareTag("Stamina") || other.CompareTag("DualKome"))
      {
         return true;
      }
      else
      {
         return false;
      }
   }
}
