using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreData : MonoBehaviour
{
    [SerializeField]
    public static int _HighScore = 0;

    public Text ScoreText;

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "" + _HighScore.ToString();
    }
    /// <summary>
    /// �{�^����Click��������Score�㏸
    /// </summary>
    public void OnClickButton()
    {
        Debug.Log("�{�^�����N���b�N����܂����B");

        //Score��1�㏸������
        _HighScore++;
    }
    public static int getscore()
    {
        return _HighScore;
    }
}
