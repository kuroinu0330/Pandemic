using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbNormalBreadEX : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private List<Sprite> _sprite;

    private void Start()
    {
        // Vector2 vec = new Vector2(1, -1);
        // StartCoroutine(Move(vec.normalized));
        StartCoroutine(Animation());
    }

    public IEnumerator Move(Vector2 direction)
    {
        float timer = 0.0f;
        
        while (timer <= 30f)
        {
            _rigidbody.velocity = direction * _moveSpeed;
            timer += Time.deltaTime;
            yield return null;
        }
        BreadManager.Instance.BreadIndividualEXSub();
        Destroy(this.gameObject);
        yield break;
    }

    private IEnumerator Animation()
    {
        float timer = 0.0f;
        int count = 0;
        
        while (true)
        {
            if (timer >= 0.05f)
            {
                count++;
                if (count >= _sprite.Count)
                {
                    count = 0;
                }
                _spriteRenderer.sprite = _sprite[count];
                timer = 0.0f;
            }

            timer += Time.deltaTime;

            yield return null;
        }
        
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            BreadManager.Instance.TouchPlayer();
        }
    }
}
