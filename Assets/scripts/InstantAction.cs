using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM(0);
    }
}
