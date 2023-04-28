using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seni : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        StartCoroutine("OnClickStartButton");
    }

    IEnumerator OnClickStartButton()
    {
        //3ïbí‚é~
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
