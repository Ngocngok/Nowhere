using Nowhere.Interaction;
using Nowhere.Item;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour, IInteractable
{
    [Title("Config")]
    [SerializeField] private InteractionConfig _interactionConfig;
    public InteractionConfig InteractionConfig => _interactionConfig;

    private ComponentRepository _componentRepository;
    private Item _item;

    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _item = _componentRepository.GetComponent<Item>();
    }
    public bool IsCompatibleWithInteracter(IInteracter interacter)
    {
        return interacter is Eater;
    }

    public bool IsInteractable(IInteracter interacter)
    {
        Picker picker = interacter as Picker;
        return picker.CanPickup;
    }

    public void OnInteract(IInteracter interacter, Action onInteractionStart, Action onInteractionDone)
    {
        onInteractionStart?.Invoke();

        onInteractionDone?.Invoke();
    }
}
