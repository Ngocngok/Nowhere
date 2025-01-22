using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;
using UnityEditor;
using DG.Tweening;
using Nowhere.Item;
using R3;
using Nowhere.Interaction;

[RequireComponent(typeof(Outlinable))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] private float _interval = 1;

    private ComponentRepository _componentRepository;
    private CharacterDetection _playerDetection;
    private Outlinable _outlinable;
    private Tween _dialateTween;

    private readonly CompositeDisposable _characterDetectionObservable = new();

    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _outlinable = _componentRepository.GetCachedComponent<Outlinable>();
        _playerDetection = _componentRepository.GetCachedComponent<CharacterDetection>();
        StopHighlight();
    }

    public void StartHighlight()
    {
        _outlinable.enabled = true;
        _outlinable.OutlineParameters.DilateShift = 0;
        _dialateTween = _outlinable.OutlineParameters.DODilateShift(1, _interval)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopHighlight()
    {
        if (_dialateTween.IsActive())
        {
            _dialateTween.Kill();
            _dialateTween = null; 
        }
        _outlinable.OutlineParameters.DilateShift = 0;
        _outlinable.enabled = false;
    }

    private void Reset()
    {
        _outlinable = GetComponent<Outlinable>();
    }

    private void OnEnable()
    {
        //_playerDetection.OnCharacterEnterObservable.Subscribe(_ => StartHighlight()).AddTo(_characterDetectionObservable);
        //_playerDetection.OnCharacterExitObservable.Subscribe(_ => StopHighlight()).AddTo(_characterDetectionObservable);
    }

    private void OnDisable()
    {
        StopHighlight();
        _characterDetectionObservable.Clear();
    }
}
