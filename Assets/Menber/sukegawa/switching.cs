using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switching : MonoBehaviour
{
    //this.gameObject.SetActive(false);
    ///<summary>
    ///接触した時
    ///</summary>
    ///<param name="collision"></param>
    void Start(){
        Invoke("ChangeScene",1.0f);
        StartCoroutine("firststop");
    }
    //触れた後の動き
    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bread")
        {
            this.gameObject.SetActive(true);
        }

        if (this.gameObject.SetActive = true)
        {
            void ChangeScene()
                {
                    SceneManager.LoadScene("MainScene");//sceneの変更
                    bread.instance.breadIn();//breadの引用
                }
        }   
    }*/

}