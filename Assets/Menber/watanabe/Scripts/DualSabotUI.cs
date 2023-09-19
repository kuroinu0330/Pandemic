using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualSabotUI : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void DualSabotUITrue()
    {
        //this.gameObject.SetActive(true);
        anim.SetBool("BoBool", true);
    }
    public void DualSabotUIfalse()
    {
        this.gameObject.GetComponent<Animator>().SetBool("BoBool", false);
    }
}
