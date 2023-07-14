using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Sprite _on;
    [SerializeField] private Sprite _off;
    [SerializeField] private bool _isOn;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Toggle);
    }

    public void Toggle()
    {
        _isOn = !_isOn;
        ForceImpact();
    }

    private void ForceImpact()
    {
        _button.image.sprite = _isOn ? _on : _off;
    }

    public void OnValidate()
    {
        _button = GetComponent<Button>();
        ForceImpact();
    }


}
