using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;
using UnityEditor;
using DG.Tweening;
using Nowhere.Item;

[RequireComponent(typeof(Outlinable))]
public class Highlighter : MonoBehaviour
{
    [SerializeField] private Outlinable outlinable;

    [SerializeField] private float interval = 1;

    private ComponentRepository componentRepository;
    private PlayerDetection playerDetection;
    private Tween dialateTween;

    private void Awake()
    {
        componentRepository = GetComponentInParent<ComponentRepository>();
        playerDetection = componentRepository.GetCachedComponent<PlayerDetection>();
        Debug.Log(componentRepository.GetCachedComponent<Item>());
        StopHighlight();
    }

    public void StartHighlight()
    {
        Debug.Log("a");
        outlinable.enabled = true;
        outlinable.OutlineParameters.DilateShift = 0;
        dialateTween = outlinable.OutlineParameters.DODilateShift(1, interval)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopHighlight()
    {
        Debug.Log("b");
        if (dialateTween.IsActive())
        {
            dialateTween.Kill();
            dialateTween = null; 
        }
        outlinable.OutlineParameters.DilateShift = 0;
        outlinable.enabled = false;
    }

    private void Reset()
    {
        outlinable = GetComponent<Outlinable>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        StopHighlight();
    }
}
