using DG.Tweening;
using Nowhere.Interaction;
using Nowhere.Item;
using Sirenix.OdinInspector;
using R3;
using UnityEngine;
using System;
using UnityEngine.AI;
using Nowhere.Helper;

public class Pickable : MonoBehaviour, IInteractable
{
    [Title("Config")]
    [SerializeField] private InteractionConfig _interactionConfig;
    [SerializeField] private PickupableConfig _pickupableConfig;
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
        return interacter is Picker;
    }

    public bool IsInteractable(IInteractor interacter)
    {
        Picker picker = interacter as Picker;
        return picker.CanPickup;
    }

    public void OnInteract(IInteractor interacter, Action onInteractionStart, Action onInteractionDone)
    {
        onInteractionStart?.Invoke();
        Picker picker = interacter as Picker;

        // Get a position for picker to pickup this
        Vector3 destination = interacter.ComponentRepository.transform.position;
        float randomDirection = UnityEngine.Random.Range(0f, Mathf.PI);
        if(NavMesh.SamplePosition(_entity.transform.position + new Vector3(Mathf.Cos(randomDirection), 0, Mathf.Sin(randomDirection)) * 2, out NavMeshHit hit, 2, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        // Get facing direction
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, _entity.transform.position.WithoutY() - destination.WithoutY());

        IMovement pickerMovement = picker.ComponentRepository.GetCachedComponent<IMovement>();
        pickerMovement.ProcedureMoveTo(destination, rotation, () =>
        {
            EntityPickup pickupItem = Instantiate(_pickupableConfig.entityVisualPrefab, _entity.transform.position, _entity.transform.rotation);

            pickupItem.transform.DOJump(picker.GetPickupItemPositon(), 1, 1, .7f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                picker.AddItem(pickupItem.gameObject, pickupItem.transform);
                pickerMovement.EndProcedureMove();
                onInteractionDone?.Invoke();
            });



            Destroy(_entity.gameObject);
        });

    }
}
