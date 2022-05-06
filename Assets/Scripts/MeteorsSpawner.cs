using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeteorsSpawner : MonoBehaviour
{
    [SerializeField] private List<Meteor> _meteorsPrefabs;
    [SerializeField] private GameObject _explosion;
    private List<Meteor> _meteors = new List<Meteor>();
    private Vector2 _bounds => GameManager.Bounds;
    private List<Player> _players => GameManager.Players;
    [SerializeField] private int _maxCount;
    [SerializeField] private float _minImpulse;
    [SerializeField] private float _maxImpulse;

    private void Start()
    {
        Spawn(Mathf.RoundToInt(_maxCount / 2));
        StartCoroutine(SpawnRoutine());
    }

    private void FixedUpdate()
    {
        for (int i = _meteors.Count - 1; i >= 0; i--)
        {
            Meteor item = _meteors[i];
            if (Vector3.Distance(item.transform.position, transform.position) > Mathf.Max(_bounds.x, _bounds.y))
            {
                _meteors.Remove(item);
                Destroy(item.gameObject);
            }
        }
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            if (_meteors.Count < _maxCount)
            {
                Spawn();
            }
        }
    }

    private void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn(true);
        }
    }

    private void Spawn(bool initial = false)
    {
        if (initial == true)
        {
            Vector2 position = Vector2.zero;

            do
            {
                position.x = Random.Range(0, _bounds.x) - _bounds.x / 2;
                position.y = Random.Range(0, _bounds.y) - _bounds.y / 2;
            } while (CheckPositionForSpawn(position) == false);

            Meteor meteor = Instantiate(_meteorsPrefabs[Random.Range(0, _meteorsPrefabs.Count)], position, transform.rotation, transform);
            meteor.Rigidbody2D.AddTorque(Random.Range(-10, 10));
            meteor.Rigidbody2D.AddForce(Extentions.RandomVector2() * Random.Range(_minImpulse, _maxImpulse), ForceMode2D.Impulse);
            _meteors.Add(meteor);
        }
        else
        {
            Vector2 position = Vector2.zero;
            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0:
                    position.x = Random.Range(0, _bounds.x) - _bounds.x / 2;
                    position.y = -_bounds.y / 2;
                    break;
                case 1:
                    position.x = Random.Range(0, _bounds.x) - _bounds.x / 2;
                    position.y = _bounds.y / 2;
                    break;
                case 2:
                    position.x = _bounds.x / 2;
                    position.y = Random.Range(0, _bounds.y) - _bounds.y / 2;
                    break;
                case 3:
                    position.x = -_bounds.x / 2;
                    position.y = Random.Range(0, _bounds.y) - _bounds.y / 2;
                    break;
            }

            Meteor meteor = Instantiate(_meteorsPrefabs[Random.Range(0, _meteorsPrefabs.Count)], position, transform.rotation, transform);
            meteor.Rigidbody2D.AddTorque(Random.Range(-10, 10));
            Vector2 forceDirection = (Extentions.RandomVector2() * (_bounds.y < _bounds.x ? _bounds.y : _bounds.x) - position).normalized;
            meteor.Rigidbody2D.AddForce(forceDirection * Random.Range(_minImpulse, _maxImpulse), ForceMode2D.Impulse);
            _meteors.Add(meteor);
        }
            
        
    }

    public bool CheckPositionForSpawn(Vector2 position)
    {
        bool crossingPlayers =  _players.Any(x => Vector3.Distance(x.transform.position, new Vector3(position.x, position.y)) < 1f) == false;
        bool crossingMeteors = _meteors.Any(x => Vector3.Distance(x.transform.position, new Vector3(position.x, position.y)) < 1f) == false;
        return crossingPlayers || crossingMeteors;
    }

    public void DestroyMeteor(Meteor meteor)
    {
        _meteors.Remove(meteor);
        GameObject explosion = Instantiate(_explosion);
        explosion.transform.position = meteor.transform.position;
        Destroy(meteor.gameObject);
    }
}
