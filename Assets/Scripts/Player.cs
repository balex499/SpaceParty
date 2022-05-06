using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int ID;
    public Camera LinkedCamera;
    public UnityEvent<int> OnDamaged;
    private int _health = 3;
    public int Health
    {
        get => _health;
        set
        {
            OnDamaged?.Invoke(value);
            if (value == 0)
            {
                DestroyShip();
            }
            _health = value;
        }
    }

    private void Awake()
    {
        LinkedCamera.GetComponent<FollowPlayer>().SetTarget(transform);        
    }

    public void TakeDamage()
    {
        Health--;
    }

    public void DestroyShip()
    {
        GameObject explosion = Instantiate(Resources.Load("VFX/Explosion", typeof(GameObject))) as GameObject;
        explosion.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
