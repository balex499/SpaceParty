using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private EnemyShipAI _enemyShipAI;

    [SerializeField] private List<Weapon> _weapons;
    private Weapon _current;

    private bool _available = true;

    private void Awake()
    {
        _enemyShipAI = GetComponent<EnemyShipAI>();
        _current = _weapons[0];
    }
   
    public void Shoot()
    {
            if (_available == true)
            {
                foreach (var initialPoint in _current.spawnPoints)
                {
                    Projectile projectile = Instantiate(_current.projectile, initialPoint.position, initialPoint.rotation);
                    projectile.Init(_enemyShipAI.Direction.normalized, _current.damage);
                }
                _available = false;
                StartCoroutine(Cooldown(_current.cooldown));
            }
    }

    private IEnumerator Cooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        _available = true;
    }
}
