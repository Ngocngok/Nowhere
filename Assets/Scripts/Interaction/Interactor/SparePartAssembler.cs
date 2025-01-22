using DG.Tweening;
using Nowhere.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SparePartAssembler : MonoBehaviour, IInteractor
{

    private EventHandler _eventHandler;
    private ComponentRepository _componentRepository;
    public ComponentRepository ComponentRepository => _componentRepository;

    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _eventHandler = _componentRepository.GetCachedComponent<EventHandler>();
    }

    public SparePartType CurrentPart()
    {
        _componentRepository.TryGetCachedComponent(out Picker picker);
        picker.TryPeekTopItem(out GameObject item);
        item.TryGetComponent(out SparePart sparePart);
        return sparePart.partType;
    }

    public bool CanAssemble(params SparePartType[] availableSparePartPositions)
    {
        if (!_componentRepository.TryGetCachedComponent(out Picker picker))
        {
            return false;
        }

        if (!picker.TryPeekTopItem(out GameObject item))
        {
            return false;
        }

        if (!item.TryGetComponent(out SparePart sparePart))
        {
            return false;
        }

        return availableSparePartPositions.Contains(sparePart.partType);
    }

    public void AssembleSparePart(Transform sparePartPosition, Action<SparePartAssembled> onAssembleAnimationDone)
    {
        Vector3 destination = ComponentRepository.transform.position.WithoutY();
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, sparePartPosition.position.WithoutY() - destination);

        if (NavMesh.SamplePosition(sparePartPosition.position + (destination - sparePartPosition.position.WithoutY()).normalized * 2, out NavMeshHit hit, 2, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        Picker picker = ComponentRepository.GetCachedComponent<Picker>();

        IMovement growerMovement = ComponentRepository.GetCachedComponent<IMovement>();
        growerMovement.ProcedureMoveTo(destination, rotation, () =>
        {
            SparePart spareItem = picker.PopTopItem().GetComponent<SparePart>();
            spareItem.transform.DOJump(sparePartPosition.position, 1, 1, .7f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                growerMovement.EndProcedureMove();
                onAssembleAnimationDone?.Invoke(spareItem.sparePartAssembled);
                Destroy(spareItem.gameObject);
            });

            _eventHandler.ExecuteEvent(PlayerEvent.AfterPlayerInteract);

        });

    }
}
