using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimetionTest : MonoBehaviour
{
    private Animator _aniem;
    // Start is called before the first frame update
    void Start()
    {
        _aniem = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _aniem.SetBool("bread Bool", true);
        }
    }

    private void Animation()
    {
        _aniem.SetBool("bread Bool", false);
    }

}
