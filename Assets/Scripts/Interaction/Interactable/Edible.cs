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

public class Edible : MonoBehaviour, IInteractable
{
    [Title("Config")]
    [SerializeField] private InteractionConfig _interactionConfig;
    public InteractionConfig InteractionConfig => _interactionConfig;

    private ComponentRepository _componentRepository;
    private Entity _entity;

    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _entity = _componentRepository.GetComponent<Entity>();
    }
    public bool IsCompatibleWithInteractor(IInteractor interacter)
    {
        return interacter is Eater;
    }

    public bool IsInteractable(IInteractor interacter)
    {
        Eater eater = interacter as Eater;
        return eater.CanEat;
    }

    public void OnInteract(IInteractor interacter, Action onInteractionStart, Action onInteractionDone)
    {
        onInteractionStart?.Invoke();
        Eater eater = interacter as Eater;

        // Get a position for picker to pickup this
        Vector3 destination = interacter.ComponentRepository.transform.position;
        if (NavMesh.SamplePosition(_entity.transform.position + (destination - _entity.transform.position).normalized, out NavMeshHit hit, 2, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        // Get facing direction
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, _entity.transform.position.WithoutY() - destination.WithoutY());

        IMovement eaterMovement = eater.ComponentRepository.GetCachedComponent<IMovement>();
        eaterMovement.ProcedureMoveTo(destination, rotation, () =>
        {
            eater.Eat(_entity.EntityConfig, () =>
            {
                eaterMovement.EndProcedureMove();   
                onInteractionDone?.Invoke();
                Destroy(_entity.gameObject);
            });
        });

    }
}
