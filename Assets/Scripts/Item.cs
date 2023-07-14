using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour, ICollectable
{
    private protected Player _target;
    public Player Target => _target;
    public virtual void Pick()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _target = player;
            Pick();
        }
    }

}
