using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipAI : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float _rotationSpeed = 4f;
    [SerializeField] private float _forcePower = 2f;
    [SerializeField] private float _speedLimit = 2f;
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _direction = Vector3.up;
    private EnemyGun _enemyGun;

    [Header("VFX")]
    [SerializeField] private float _rateOverTimeLimit = 100f;
    [SerializeField] private ParticleSystem _engineVFX;
    public Vector3 Direction
    {
        get => _direction;
        private set
        {
            if (value == Vector3.zero)
            {
                return;
            }
            else
            {
                _direction = value;
            }
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.inertia = 0.2f;
        _enemyGun = GetComponent<EnemyGun>();
    }

    void FixedUpdate()
    {
        Sense();
        Rotate();

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > 3f)
        {
            Force();
        }

        if (distance < 5f)
        {
            _enemyGun.Shoot();
        }
    }

    float inputForce;
    private void Sense()
    {
        Direction = target.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Direction, 5f, _layerMask);
        
        if (hit.collider != null)
        {
            Debug.Log("Has Hits " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.TryGetComponent(out Meteor meteor))
            {
                Debug.Log("Shoot");
                if (Vector3.Distance(transform.position, meteor.transform.position) < 5f)
                {
                    _enemyGun.Shoot();
                }
            }
        }

        
        Direction.Normalize();
        inputForce = 1f;
    }

    private void Rotate()
    {
        
        Quaternion to = Quaternion.LookRotation(Vector3.forward, Direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, _rotationSpeed);
    }


    private void Force()
    {
        EngineVFX(inputForce);

        if (inputForce == 0)
        {
            return;
        }
        _rigidbody2D.AddForce(Direction * inputForce * _forcePower, ForceMode2D.Force);

        if (_rigidbody2D.velocity.magnitude > _speedLimit)
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _speedLimit;
        }

    }

    private void EngineVFX(float inputForce)
    {
        var emission = _engineVFX.emission;
        emission.rateOverTime = inputForce * _rateOverTimeLimit;
    }

}
