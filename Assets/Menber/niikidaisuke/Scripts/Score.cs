using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public int _HighScore;
    // Start is called before the first frame update
    void Start()
    {
        //_HighScore = RiceBollsMoveTest.getscore();

        //_HighScore = GameSceneIndex.instance.GetGameSceneScore();

        //ScoreText.text = string.Format("{0}", _HighScore);

    }
}
