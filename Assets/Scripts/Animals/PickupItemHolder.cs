using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemHolder : MonoBehaviour
{
    [SerializeField] private Transform _referenceBone;
    
    private Transform _rootHolder;
    //private Vector3 _rootHolderInitialPosition;
    private Vector3 _referenceBoneInitialPosition;
    private Quaternion _rootHolderInitialRotation;
    private Quaternion _referenceBoneInitialRotation;

    private Transform[] _holders;
    private Vector3[] _holderInitialPositions;

    private void Awake()
    {
        _rootHolder = transform;
        //_rootHolderInitialPosition = _rootHolder.localPosition;
        _rootHolderInitialRotation = _rootHolder.localRotation;
        _referenceBoneInitialRotation = _referenceBone.localRotation;

        _holders = new Transform[_rootHolder.childCount];
        for (int i = 0; i < _holders.Length; i++)
        {
            _holders[i] = _rootHolder.GetChild(i);
        }

        _referenceBoneInitialPosition = _referenceBone.localPosition;

        _holderInitialPositions = new Vector3[_holders.Length];
        for (int i = 0; i < _holderInitialPositions.Length; i++)
        {
            _holderInitialPositions[i] = _holders[i].localPosition;
        }
    }

    Vector3 deltaPosition;
    void Update()
    {
        _rootHolder.localScale = _referenceBone.localScale;
        _rootHolder.localRotation = Quaternion.Slerp(_rootHolderInitialRotation, _rootHolderInitialRotation * (_referenceBone.localRotation * Quaternion.Inverse(_referenceBoneInitialRotation)), .8f);

        for (int i = 0; i < _holders.Length; i++)
        {
            if(i == 0)
            {
                deltaPosition = _referenceBone.localPosition - _referenceBoneInitialPosition;

            }
            else
            {
                deltaPosition = _holders[i - 1].localPosition - _holderInitialPositions[i - 1];
            }

            _holders[i].localPosition = Vector3.Lerp(_holders[i].localPosition, _holderInitialPositions[i] + deltaPosition, .9f);

        }
    }
}
