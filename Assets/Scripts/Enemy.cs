using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health = 1;
    public int Health
    {
        get => _health;
        set
        {
            if (value == 0)
            {
                DestroyShip();
            }
            _health = value;
        }
    }

    public void TakeDamage()
    {
        Health--;
    }

    public void DestroyShip()
    {
        GameObject explosion = Instantiate(Resources.Load("VFX/Explosion", typeof(GameObject))) as GameObject;
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}
