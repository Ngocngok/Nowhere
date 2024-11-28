using Nowhere.Utility;
using R3;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Nowhere.Utility.QuickSelect2;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{
    [SerializeField] private LayerMask targetLayer;

    private readonly RaycastHit[] _hits = new RaycastHit[10];
    private Camera _mainCamera;
    private RayCastHitRefComparer _rayCastHitRefComparer;


    private readonly Subject<Vector3> _groundHitAsObservable = new();
    public Observable<Vector3> GroundHitAsObservable => _groundHitAsObservable;

    private void Awake()
    {
        _mainCamera = Camera.main;
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

            int hits = Physics.RaycastNonAlloc(_mainCamera.ScreenPointToRay(Input.mousePosition), _hits, 100, targetLayer);

            if (hits > 0)
            {
                //
                RaycastHit firstHit = SmallestKRefCompare(_hits, hits, 0, _rayCastHitRefComparer);

                //proceed target hit

                if(firstHit.collider.TryGetComponent(out IPointerClickHandler pointerClickHandler))
                {
                    pointerClickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
                }
                else
                {
                    _groundHitAsObservable.OnNext(_hits[0].point);
                }
            }
        }
    }

    public void RegisterGroundHitAsObservable(Action<Vector3> handler)
    {
        GroundHitAsObservable.Subscribe(handler);
    }

    private struct RayCastHitRefComparer : IRefComparer<RaycastHit>
    {
        public int Compare(ref RaycastHit x, ref RaycastHit y)
        {
            return (int)(x.distance * 10 - y.distance * 10);
        }
    }

}
