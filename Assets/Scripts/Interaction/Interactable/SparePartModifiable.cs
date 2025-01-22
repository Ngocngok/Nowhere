using Nowhere.Interaction;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SparePartModifiable : MonoBehaviour, IInteractable
{

    [Title("Config")]
    [SerializeField] private InteractionConfig _interactionConfig;
    public InteractionConfig InteractionConfig => _interactionConfig;

    [Title("SparePart")]
    public SparePartPosition[] sparePartPositions;
    
    
    private bool[] _sparePartOccupied;
    private SparePartAssembled[] _assembledSpareParts;
    private ComponentRepository _componentRepository;
    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _sparePartOccupied = new bool[sparePartPositions.Length];
        _assembledSpareParts = new SparePartAssembled[sparePartPositions.Length];
    }


    public bool IsCompatibleWithInteractor(IInteractor interactor)
    {
        if (interactor is not SparePartAssembler sparePartAssembler)
        {
            return false;
        }


        List<SparePartType> unOccupiedSparePartPosition = new List<SparePartType>();
        for (int i = 0; i < _sparePartOccupied.Length; i++)
        {
            if (!_sparePartOccupied[i])
            {
                unOccupiedSparePartPosition.Add(sparePartPositions[i].sparePartType);
            }
        }

        return sparePartAssembler.CanAssemble(unOccupiedSparePartPosition.ToArray());
    }

    public bool IsInteractable(IInteractor interacter)
    {
        SparePartAssembler sparePartAssembler = interacter as SparePartAssembler;

        List<SparePartType> unOccupiedSparePartPosition = new List<SparePartType>();
        for (int i = 0; i < _sparePartOccupied.Length; i++)
        {
            if (!_sparePartOccupied[i])
            {
                unOccupiedSparePartPosition.Add(sparePartPositions[i].sparePartType);
            }
        }

        return sparePartAssembler.CanAssemble(unOccupiedSparePartPosition.ToArray());
    }

    private Action _onInteractionDone;

    public void OnInteract(IInteractor interacter, Action onInteractionStart, Action onInteractionDone)
    {
        onInteractionStart?.Invoke();
        SparePartAssembler sparePartAssembler = interacter as SparePartAssembler;

        SparePartType partType = sparePartAssembler.CurrentPart();

        Transform partPosition = sparePartPositions.First(x => x.sparePartType == partType).sparePartHolder;

        sparePartAssembler.AssembleSparePart(partPosition, plant =>
        {
            OnAnimationAssembleEnd(partType, plant);
        });

        _onInteractionDone = onInteractionDone;

    }
    private void OnAnimationAssembleEnd(SparePartType partType, SparePartAssembled sparePartAssembled)
    {
        int indexOfSparePartToSpawn = IndexOfFirstAvailableSparePart(partType);

        _assembledSpareParts[indexOfSparePartToSpawn] = Instantiate(sparePartAssembled, sparePartPositions[indexOfSparePartToSpawn].sparePartHolder);

        FlyingBroomVehicle flyingBroomVehicle = _componentRepository.GetCachedComponent<FlyingBroomVehicle>();
        flyingBroomVehicle.RegisterBattery(sparePartAssembled);
        _onInteractionDone?.Invoke();
    }


    private int IndexOfFirstAvailableSparePart(SparePartType partType)
    {
        for (int i = 0; i < sparePartPositions.Length; i++)
        {
            if (sparePartPositions[i].sparePartType == partType && _assembledSpareParts[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    [Serializable]
    public struct SparePartPosition
    {
        public Transform sparePartHolder;
        public SparePartType sparePartType;
    }

}

