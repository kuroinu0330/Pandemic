using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestEnemyCount : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI _text;
   
   private int _enemyCount = 0;

   public static TestEnemyCount instance;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         _text = this.gameObject.GetComponent<TextMeshProUGUI>();
      }
   }

   public void countUp()
   {
      _enemyCount++;
      _text.text = _enemyCount.ToString();
   }

}
