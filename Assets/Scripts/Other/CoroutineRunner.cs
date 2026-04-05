using System;
using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    public Coroutine Run(IEnumerator coroutine)
    {
        if (coroutine == null) throw new ArgumentNullException(nameof(coroutine));
        return StartCoroutine(coroutine);
    }

    public void Stop(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
