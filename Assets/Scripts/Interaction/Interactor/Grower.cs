using DG.Tweening;
using Nowhere.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grower : MonoBehaviour, IInteractor
{
    private EventHandler _eventHandler;

    private ComponentRepository _componentRepository;
    public ComponentRepository ComponentRepository => _componentRepository;
    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _eventHandler = _componentRepository.GetCachedComponent<EventHandler>();
    }

    public bool CanGrow()
    {
        if(!_componentRepository.TryGetCachedComponent(out Picker picker))
        {
            return false;
        }

        if(!picker.TryPeekTopItem(out GameObject item))
        {
            return false;
        }

        return item.TryGetComponent(out Seed _);
    }

    public void Plant(Transform seedPosition, Action<PlantGrowing> onPlantAnimationDone)
    {
        Vector3 destination = ComponentRepository.transform.position.WithoutY();
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, seedPosition.position.WithoutY() - destination);

        if (NavMesh.SamplePosition(seedPosition.position + (destination - seedPosition.position.WithoutY()).normalized * 2, out NavMeshHit hit, 2, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        Picker picker = ComponentRepository.GetCachedComponent<Picker>();

        IMovement growerMovement = ComponentRepository.GetCachedComponent<IMovement>();
        growerMovement.ProcedureMoveTo(destination, rotation, () =>
        {
            Seed seedItem = picker.PopTopItem().GetComponent<Seed>();
            seedItem.transform.DOJump(seedPosition.position, 1, 1, .7f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                growerMovement.EndProcedureMove();
                onPlantAnimationDone?.Invoke(seedItem.PlantableConfig.plantGrowingPrefab);
                Destroy(seedItem.gameObject);
            });

            _eventHandler.ExecuteEvent(PlayerEvent.AfterPlayerInteract);

        });
    }
}
