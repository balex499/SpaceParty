using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Vector2 _bounds;
    public static Vector2 Bounds => Instance._bounds;

    [SerializeField] private List<Player> _players = new List<Player>();
    public static List<Player> Players => Instance._players;

    [SerializeField] private RectTransform _miniMap;
    [SerializeField] private GameObject _secondPlayerUI;

    private bool _twoPlayersMode;
    private bool _p2PlayButtonBlocked = false;

    private void Awake()
    {
        Instance = this;
        SetTwoPlayersMode(false);
        InitializeMode();
    }

    private void Start()
    {
        GeneralUI.StartTimer(80);
    }

    private void Update()
    {
        if (Input.GetAxis("Player2Start") == 1 && _p2PlayButtonBlocked == false)
        {
            _p2PlayButtonBlocked = true;

            SetTwoPlayersMode(!_twoPlayersMode);
        }

        if (Input.GetAxis("Player2Start") == 0 && _p2PlayButtonBlocked == true)
        {
            _p2PlayButtonBlocked = false;
        }
    }
    private void InitializeMode()
    {
        int mode = 0;
        switch (mode)
        {
            case 0:

                break;
        }
    }

    private void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }

    private void SetTwoPlayersMode(bool status)
    {
        _players[1].gameObject.SetActive(status);
        _players[1].LinkedCamera.gameObject.SetActive(status);
        _players[0].LinkedCamera.rect = status ? new Rect(Vector2.zero, new Vector2(0.5f, 1)) : new Rect(Vector2.zero, Vector2.one);
        _twoPlayersMode = !_twoPlayersMode;
        _secondPlayerUI.SetActive(status);
        _miniMap.anchoredPosition = status ? new Vector2(0, 130) : new Vector2(560, 130);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        Vector2 position = new Vector2(-_bounds.x * 0.5f, -_bounds.y * 0.5f);
        DrawRect(new Rect(position, _bounds));
    }
}
