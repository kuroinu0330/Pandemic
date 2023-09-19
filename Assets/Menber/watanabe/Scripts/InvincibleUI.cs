using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleUI : MonoBehaviour
{
    public Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
    }

    public void InvincibleAnimetionTrue()
    {
        //this.gameObject.SetActive(true);
        _anim.SetBool("InBool", true);
    }
    public void InvincibleAnimetionfalse()
    {
        this.gameObject.GetComponent<Animator>().SetBool("InBool", false);
    }
}
