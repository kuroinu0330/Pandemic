using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game: MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("TestSceneT001");
    }
}