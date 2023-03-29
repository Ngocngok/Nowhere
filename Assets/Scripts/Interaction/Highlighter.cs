using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;
using UnityEditor;
using DG.Tweening;

[RequireComponent(typeof(Outlinable))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] private Outlinable outlinable;

    [SerializeField] private float interval = 1;

    Tween dialateTween;

    private void OnEnable()
    {
        dialateTween = outlinable.OutlineParameters.DODilateShift(0, interval)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        dialateTween.Kill();
    }

    private void Awake()
    {
        outlinable.enabled = false;
    }

    private void Reset()
    {
        outlinable = GetComponent<Outlinable>();
    }
}
