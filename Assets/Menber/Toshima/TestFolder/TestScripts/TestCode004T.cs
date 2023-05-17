using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode004T : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0f);
    }
}
