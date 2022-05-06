using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static Vector2 RandomVector2()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
