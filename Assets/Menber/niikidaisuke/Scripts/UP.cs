//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class UP : MonoBehaviour
//{

//    [SerializeField] int m_Start;   // �J�n�l
//    [SerializeField] int m_Goal;    // �ŏI�ړI�l
//    [SerializeField] int m_Time;    // �K�v�b��

//    private Text m_Text;            // uGUI/Text
//    //private CountTo m_CountTo;      // �J�E���g�A�b�v(�܂��̓_�E��)�������s���N���X

//    // Use this for initialization
//    void Start()
//    {
//        m_Text = this.GetComponent<Text>();
//        m_CountTo = this.gameObject.AddComponent<CountTo>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // CountTo�N���X�����쒆�Ȃ�΁ACanvas/Text���X�V����
//        if (m_CountTo.IsWorking())
//        {
//            int value = (int)Mathf.Ceil(m_CountTo.Value);
//            m_Text.text = value.ToString();
//        }
//        // CountTo�N���X�����삵�Ă��Ȃ��Ȃ�΁AEnter�L�[�������g���K�[�ɂ��ăJ�E���g�A�b�v(�܂��̓_�E��)���J�n����
//        else
//        {
//            if (Input.GetKeyDown(KeyCode.Return))
//            {
//                m_CountTo.CountToInt(m_Start, m_Goal, m_Time);
//            }
//        }
//    }
//}