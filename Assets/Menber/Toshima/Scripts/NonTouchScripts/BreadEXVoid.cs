using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadEXVoid : MonoBehaviour
{
   [SerializeField]
   private NormalBreadEx scripts;
   
   /// <summary>
   /// 検出判定
   /// </summary>
   /// <param name="other"></param>
   private void OnTriggerEnter2D(Collider2D other)
   {
      //米粒タグの場合以下の処理を実行する
      if (other.gameObject.tag == "RiceBaby")
      {
         //米粒オブジェクトを配列に叩き込む
         scripts.ListAdd(other.gameObject);
      }
   }

   /// <summary>
   /// 喪失判定
   /// </summary>
   /// <param name="other"></param>
   private void OnTriggerExit2D(Collider2D other)
   {
      //米粒タグの場合以下の処理を実行する
      if (other.gameObject.tag == "RiceBaby")
      {
         //米粒オブジェクトを配列から叩き出す
         scripts.ListRemove(other.gameObject);
      }
   }
}
