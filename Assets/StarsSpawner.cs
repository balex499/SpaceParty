using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsSpawner : MonoBehaviour
{
    [SerializeField] private Star _star;
    [SerializeField] private int _concurrentCount;
    private Vector2 _bounds => GameManager.Bounds;

    private void Start()
    {
        for (int i = 0; i < _concurrentCount; i++)
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        Vector2 position = Vector2.zero;

        position.x = Random.Range(0, _bounds.x) - _bounds.x / 2;
        position.y = Random.Range(0, _bounds.y) - _bounds.y / 2;

        Star star = Instantiate(_star, position, transform.rotation, transform);
        star.onPickedUp.AddListener(OnStarPickedUp);
        star.onPickedUp.AddListener(GeneralUI.AddStar);
    }

    public void OnStarPickedUp(Star pickedStar)
    {
        //Score pickedStar.Target++
        pickedStar.onPickedUp.RemoveListener(OnStarPickedUp);
        pickedStar.onPickedUp.RemoveListener(GeneralUI.AddStar);
        Spawn();
    }
}
