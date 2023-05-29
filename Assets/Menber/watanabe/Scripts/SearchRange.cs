using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRange : MonoBehaviour
{
    private CircleCollider2D _collider;
    [SerializeField]
    private int _sizeCount;

    public int size;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {   

    }


}
