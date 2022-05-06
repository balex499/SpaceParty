using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform _target;
    private Vector2 _limits => GameManager.Bounds;
    private Vector3 _offset;
    private Camera _camera;
    private Rect _rect;

    private void Start()
    {
        _offset = _target.position - transform.position;
        _camera = GetComponent<Camera>();

        Vector2 position = new Vector2(-_limits.x * 0.5f, -_limits.y * 0.5f);
        _rect = new Rect(position, _limits);
        if (TryGetComponent(out SpaceLimiter spaceLimiter))
        {
            spaceLimiter.LimitMap(_rect);
        }
    }

    public void SetTarget(Transform transform)
    {
        _target = transform;
    }

    private void LateUpdate()
    {
        Vector3 nextPosition = _target.position - _offset;
        nextPosition.y = Mathf.Clamp(nextPosition.y, -_limits.y / 2 + _camera.orthographicSize, _limits.y / 2 - _camera.orthographicSize);
        float aspectRatio = _camera.aspect;
        nextPosition.x = Mathf.Clamp(nextPosition.x, -_limits.x / 2 + _camera.orthographicSize * aspectRatio, _limits.x / 2 - _camera.orthographicSize * aspectRatio);
        transform.position = nextPosition;
    }
}
