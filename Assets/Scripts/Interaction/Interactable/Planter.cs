using DG.Tweening;
using Nowhere.Helper;
using Nowhere.Interaction;
using Nowhere.Item;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Planter : MonoBehaviour, IInteractable
{
    [Title("Config")]
    [SerializeField] private InteractionConfig _interactionConfig;

    public InteractionConfig InteractionConfig => _interactionConfig;

    [SerializeField] private Transform seedPosition;

    private ComponentRepository _componentRepository;
    private Entity _entity;
    private PlantGrowing _plantGrowing;

    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _entity = _componentRepository.GetComponent<Entity>();
    }

    public bool IsCompatibleWithInteractor(IInteractor interactor)
    {
        if(interactor is not Grower grower)
        {
            return false;
        }

        return grower.CanGrow();
    }

    public bool IsInteractable(IInteractor interactor)
    {
        if(_plantGrowing != null)
        {
            return false;
        }

        if (interactor is not Grower grower)
        {
            return false;
        }

        return grower.CanGrow();
    }

    private Action _onInteractionDone;

    public void OnInteract(IInteractor interacter, Action onInteractionStart, Action onInteractionDone)
    {
        onInteractionStart?.Invoke();
        Grower grower = interacter as Grower;


        grower.Plant(seedPosition, plant =>
        {
            OnAnimationGrowEnd(plant);
        });

        _onInteractionDone = onInteractionDone;
    }

    private void OnAnimationGrowEnd(PlantGrowing plant)
    {
        _plantGrowing = Instantiate(plant, seedPosition);
        _onInteractionDone?.Invoke();
    }


}
