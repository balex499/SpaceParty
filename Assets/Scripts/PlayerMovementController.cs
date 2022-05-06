using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _forcePower = 2f;
    [SerializeField] private float _speedLimit = 2f;
    private string _horizontalAxis;
    private string _verticalAxis;
    private string _forceAxis;
    private string _fireAxis;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _direction = Vector3.up;

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
        ConnectInput();
    }

    private void ConnectInput()
    {

        int id = GetComponent<Player>().ID;
        _horizontalAxis = "J" + id + "Horizontal";
        _verticalAxis = "J" + id + "Vertical"; 
        _forceAxis = "J" + id + "Force"; 
    }

    void FixedUpdate()
    {
        Rotate();
        Force();
    }

    private void Rotate()
    {
        float x = Input.GetAxis(_horizontalAxis);
        float y = Input.GetAxis(_verticalAxis);
        Direction = new Vector3(x, y, 0);
        Quaternion to = Quaternion.LookRotation(Vector3.forward, Direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, _rotationSpeed);
    }


    private void Force()
    {
        float inputForce = Input.GetAxis(_forceAxis);
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
