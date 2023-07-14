using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Star : Item
{
    [SerializeField] private ParticleSystem _feedbackVFX;
    public int value;
    public UnityEvent<Star> onPickedUp;
    public override void Pick()
    {
        _feedbackVFX.Play();
        _feedbackVFX.transform.SetParent(null);
        onPickedUp.Invoke(this);
        base.Pick();
    }

    public IEnumerator Start()
    {
        float baseSizeX = transform.localScale.x;
        transform.localScale = Vector3.zero;
        while (transform.localScale.x < baseSizeX)
        {
            yield return null;
            transform.localScale += Vector3.one * Time.deltaTime;
        }
    }

}
