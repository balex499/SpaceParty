using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private List<Image> _images;

    private void Awake()
    {
        _images = GetComponentsInChildren<Image>().ToList();
    }
    public void SetValue(int input)
    {
        int count = Mathf.Clamp(input, 0, _images.Count);
        Debug.Log(input);
        for (int i = 0; i < _images.Count; i++)
        {
            _images[i].enabled = count > 0;
            count--;
        }
    }

}
