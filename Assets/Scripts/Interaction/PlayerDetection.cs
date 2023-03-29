using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Action OnPlayerEnter;
    public Action OnPlayerExit;

    private Collider cachedPlayerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerBehavior playerBehavior))
        {
            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerBehavior playerBehavior))
        {
            OnPlayerExit?.Invoke();
        }
    }
}
