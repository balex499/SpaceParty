using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon 
{
    public int id;
    public int damage;
    public float cooldown;
    public Projectile projectile;
    public List<Transform> spawnPoints;
}
