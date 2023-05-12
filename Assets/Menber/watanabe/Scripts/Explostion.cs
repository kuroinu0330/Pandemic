using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    float m_force;
    float m_radius;
    float m_upwards;
    Vector3 m_position;

    float m_force2p;
    float m_radius2p;
    float m_upwards2p;
    Vector3 m_position2p;

    void Start()
    {
    }

    public void blowoff(int j)
    {
        if (j == 0)
        {
            Debug.Log("Explosion!");
            m_position = this.gameObject.transform.position;

            // 範囲内のRigidbodyにAddExplosionForce
            Collider[] hitColliders = Physics.OverlapSphere(m_position, m_radius);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                var rb = hitColliders[i].GetComponent<Rigidbody>();
                float distance = Vector3.Distance(hitColliders[i].transform.position, this.gameObject.transform.position);
                if (rb)
                {
                    // カルタの枠撃っても動かないギリギリの数値のため、カルタのサイズ変更した場合はここも変えること。
 

                }

            }

        }
 
    }

}