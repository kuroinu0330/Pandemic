using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    public Text ScoreText;
    [SerializeField]
    public static int score;
    float timeLine;
    // Start is called before the first frame update
    void Start()
    {
        score = GameSceneIndex.instance.GetGameSceneScore()/*ScoreData.getscore()*/;

        ScoreText.text = string.Format("{0}", 0);
        timeLine = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLine < 1)
        {
            timeLine += Time.deltaTime;
           
            if(timeLine > 1)
            {
                timeLine = 1;
            }
            int viewScore = (int)(score * timeLine);
            ScoreText.text = string.Format("{0}", viewScore);
        }
    }
}
