using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerDetection : MonoBehaviour
{
    public Action OnPlayerEnter;
    public Action OnPlayerExit;

    private Collider cachedPlayerCollider;
    private IDisposable playerOnDisabledSubscription;

    private void OnTriggerEnter(Collider other)
    {
        if (cachedPlayerCollider == other)
        {
            OnPlayerEnter?.Invoke();
            return;
        }

        if(other.TryGetComponent(out PlayerBehavior playerBehavior))
        {
            cachedPlayerCollider = other;
            playerOnDisabledSubscription = cachedPlayerCollider.OnDisableAsObservable().Subscribe(_ =>
            {
                OnPlayerExit?.Invoke();
                playerOnDisabledSubscription?.Dispose();
                cachedPlayerCollider = null;
            });

            OnPlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(cachedPlayerCollider == other)
        {
            OnPlayerExit?.Invoke();
            return;
        }
    }
}
