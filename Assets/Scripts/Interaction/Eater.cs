using Nowhere.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour, IInteracter
{
    private ComponentRepository _componentRepository;
    public ComponentRepository ComponentRepository => _componentRepository;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = _componentRepository.GetComponent<Animator>();
    }

    public void Eat(ItemConfig itemConfig)
    {
        _animator.SetTrigger(AnimationHash.triggerEatHash);
    }
}
