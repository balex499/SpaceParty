using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private int _hp;
    public Rigidbody2D Rigidbody2D => GetComponent<Rigidbody2D>();
    private MeteorsSpawner _meteorsSpawner;
    private void Awake()
    {
        _meteorsSpawner = GetComponentInParent<MeteorsSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Projectile projectile))
        {
            Destroy(projectile.gameObject);
            _hp -= projectile.Damage;
            if (_hp <= 0)
            {
                _meteorsSpawner.DestroyMeteor(this);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage();
            _meteorsSpawner.DestroyMeteor(this);
        }
    }

}
