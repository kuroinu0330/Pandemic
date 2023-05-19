using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRange : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public void blowoff()
    {

    }
}
