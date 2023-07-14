using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test00RIZER : MonoBehaviour 
{
    [SerializeField]
    private TextMeshProUGUI text;
    
    private ScoreRankingManager.ScoreIndex _index;

    public void UpdateScore() {
        string str = "";
        for (int i = 0; i < ScoreRankingManager.instance.GetScoreListCount(); i++) 
        {
            _index = ScoreRankingManager.instance.GetScoreIndex(i);

            str += _index.Name + "  " + _index.Score + "\n";
        }

        text.text = str;
    }

    public void SortScore()
    {
        ScoreRankingManager.instance.SortingRanking();
    }

    public void AddUltScore()
    {
        _index.Name = "ULTSSE";
        _index.Score = 99;
        
        ScoreRankingManager.instance.AddScoreIndex(_index.Name,_index.Score);
    }

    public void SaveScore() 
    {
        ScoreRankingManager.instance.WriteCSVFile();
    }
}
