using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceLimiter : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D _boundsMaterial;
    public void LimitMap(Rect limit)
    {
        GameObject limits = new GameObject();
        limits.transform.position = Vector3.zero;
        limits.name = "Borders";
        limits.layer = 3;
        Vector2 offset;
        Vector2 size;

        offset.x = limit.position.x - 0.5f;
        offset.y = 0;
        size.x = 1;
        size.y = limit.height;
        AddBorder(limits, offset, size);

        offset.x = limit.position.x + limit.width + 0.5f;
        offset.y = 0;
        size.x = 1;
        size.y = limit.height;
        AddBorder(limits, offset, size);

        offset.x = 0;
        offset.y = limit.position.y + limit.height + 0.5f;
        size.x = limit.width;
        size.y = 1;
        AddBorder(limits, offset, size);

        offset.x = 0;
        offset.y = limit.position.y + 0.5f;
        size.x = limit.width;
        size.y = 1;
        AddBorder(limits, offset, size);
    }

    public void AddBorder(GameObject target, Vector2 offset, Vector2 size)
    {
        BoxCollider2D boxCollider2D = target.AddComponent<BoxCollider2D>();
        boxCollider2D.offset = offset;
        boxCollider2D.size = size;
        boxCollider2D.sharedMaterial = _boundsMaterial;
    }
}
