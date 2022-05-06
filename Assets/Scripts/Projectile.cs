using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _lifetime = 3f;
    private int _damage;
    public int Damage => _damage;
    private Rigidbody2D _rigidbody2D;
    public void Init(Vector3 direction, int damage)
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Quaternion to = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = to;
        _rigidbody2D.velocity = direction * _speed;
        _damage = damage;
        Destroy(gameObject, _lifetime);
    }
}
