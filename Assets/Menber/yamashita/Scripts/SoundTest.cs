using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            soundManager.PlayBGM(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            soundManager.PlaySE(0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            soundManager.PlaySE(1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            soundManager.DestroyAudioSource();
        }
    }
}
