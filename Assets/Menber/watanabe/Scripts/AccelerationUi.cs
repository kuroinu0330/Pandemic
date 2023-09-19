using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationUi : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void AccelerationAnimetionTrue()
    {
        //this.gameObject.SetActive(true);
        anim.SetBool("Bool", true);
    }
    public void AccelerationAnimetionfalse()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Bool", false);
    }
/*    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            anim.SetBool("Bool", true);
        }
    }*/
}
