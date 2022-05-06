using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunsController : MonoBehaviour
{
    private PlayerMovementController _playerMovementController;

    [SerializeField] private List<Weapon> _weapons;
    private Weapon _current;
    private string _fireAxis; 

    private bool _available = true;

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _current = _weapons[0];
        ConnectInput();
    }

    private void ConnectInput()
    {
        int id = GetComponent<Player>().ID;
        _fireAxis = "J" + id + "Fire";
    }
    void Update()
    {
        
        if (Input.GetAxis(_fireAxis) == 1)
        {
            if (_available == true)
            {
                foreach (var initialPoint in _current.spawnPoints)
                {
                    Projectile projectile = Instantiate(_current.projectile, initialPoint.position, initialPoint.rotation);
                    projectile.Init(_playerMovementController.Direction.normalized, _current.damage);
                }
                _available = false;
                StartCoroutine(Cooldown(_current.cooldown));
            }
        }
    }

    private IEnumerator Cooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        _available = true;
    }
}
