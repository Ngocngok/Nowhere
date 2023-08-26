using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    private readonly Dictionary<string, EventType> _animationEventDictionary = new();
    private ComponentRepository _componentRepository;
    [SerializeField] private int a;
    private EventHandler _eventHandler;
    public static readonly EventType onCharacterEat = new();
    
    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _eventHandler = _componentRepository.GetComponent<EventHandler>();
        

        _animationEventDictionary.Add("OnCharacterEat", onCharacterEat);

    }


    [AnimationEventCallback]
    private void InvokeEvent()
    {

    }


}
