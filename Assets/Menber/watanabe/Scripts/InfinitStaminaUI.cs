using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitStaminaUI : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void InfinitStaminaUITrue()
    {
        //this.gameObject.SetActive(true);
        anim.SetBool("GOBool", true);
    }
    public void InfinitStaminaUIfalse()
    {
        this.gameObject.GetComponent<Animator>().SetBool("GOBool", false);
    }
}
