using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{

    private readonly RaycastHit[] _hits = new RaycastHit[10];
    private Camera _mainCamera;
    private LayerMask _targetLayer;
    


    private readonly Subject<Vector3> _groundHitAsObservable = new();
    public IObservable<Vector3> GroundHitAsObservable => _groundHitAsObservable;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _targetLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            int hits = Physics.RaycastNonAlloc(_mainCamera.ScreenPointToRay(Input.mousePosition), _hits, 100, _targetLayer);

            if (hits > 0)
            {
                //proceed target hit

                for (int i = 0; i < hits; i++)
                {

                }

                _groundHitAsObservable.OnNext(_hits[0].point);
            }
        }
    }

    public void RegisterGroundHitAsObservable(Action<Vector3> handler)
    {
        GroundHitAsObservable.Subscribe(handler);
    }

}
