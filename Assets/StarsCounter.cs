using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class StarsCounter : MonoBehaviour
{
    public int playerID;
    private TMPro.TMP_Text _tmpText;
    private int _count = 0;

    private void Awake()
    {
        _tmpText = GetComponent<TMPro.TMP_Text>();
        _tmpText.text = _count.ToString();
    }

    public void Change(int value)
    {
        _count += value;
        _count = Mathf.Clamp(_count, 0, int.MaxValue);
        _tmpText.text = _count.ToString();
    }
}
