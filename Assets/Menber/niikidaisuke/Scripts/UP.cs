//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class UP : MonoBehaviour
//{

//    [SerializeField] int m_Start;   // 開始値
//    [SerializeField] int m_Goal;    // 最終目的値
//    [SerializeField] int m_Time;    // 必要秒数

//    private Text m_Text;            // uGUI/Text
//    //private CountTo m_CountTo;      // カウントアップ(またはダウン)処理を行うクラス

//    // Use this for initialization
//    void Start()
//    {
//        m_Text = this.GetComponent<Text>();
//        m_CountTo = this.gameObject.AddComponent<CountTo>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // CountToクラスが動作中ならば、Canvas/Textを更新する
//        if (m_CountTo.IsWorking())
//        {
//            int value = (int)Mathf.Ceil(m_CountTo.Value);
//            m_Text.text = value.ToString();
//        }
//        // CountToクラスが動作していないならば、Enterキー押しをトリガーにしてカウントアップ(またはダウン)を開始する
//        else
//        {
//            if (Input.GetKeyDown(KeyCode.Return))
//            {
//                m_CountTo.CountToInt(m_Start, m_Goal, m_Time);
//            }
//        }
//    }
//}