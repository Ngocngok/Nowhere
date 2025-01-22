using DG.Tweening;
using Nowhere.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour, IInteractor
{
    private ComponentRepository _componentRepository;
    public ComponentRepository ComponentRepository => _componentRepository;
    
    private Animator _animator;


    public bool CanEat => true;

    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();

        _animator = _componentRepository.GetCachedComponent<Animator>();
    }

    public void Eat(EntityConfig itemConfig, Action OnAnimationEatDone)
    {
        _animator.SetTrigger(AnimationHash.triggerEatHash);
        DOVirtual.DelayedCall(.2f, () => OnAnimationEatDone?.Invoke());
    }
}
