using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneIndex : MonoBehaviour
{
    public static int _gameSceneScoreIndex = 0;

    public static GameSceneIndex instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }       
    }
    public void AddScore()
    {
        _gameSceneScoreIndex++;
    }

    public int GetGameSceneScore()
    {
        return _gameSceneScoreIndex;
    }

    public void ResetGameSceneScore()
    {
        _gameSceneScoreIndex = 0;
    }
}
