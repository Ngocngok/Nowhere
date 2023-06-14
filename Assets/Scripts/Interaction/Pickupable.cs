using DG.Tweening;
using Nowhere.Interaction;
using Nowhere.Item;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using System;
using UnityEngine.AI;
using Nowhere.Helper;

public class Pickupable : MonoBehaviour, IInteractable
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
        return interacter is Picker;
    }

    public bool IsInteractable(IInteracter interacter)
    {
        Picker picker = interacter as Picker;
        return picker.CanPickup;
    }

    public void OnInteract(IInteracter interacter, Action onInteractionStart, Action onInteractionDone)
    {
        onInteractionStart?.Invoke();
        Picker picker = interacter as Picker;

        // Get a position for picker to pickup this
        Vector3 destination = interacter.ComponentRepository.transform.position;
        float randomDirection = UnityEngine.Random.Range(0f, Mathf.PI);
        if(NavMesh.SamplePosition(_item.transform.position + new Vector3(Mathf.Cos(randomDirection), 0, Mathf.Sin(randomDirection)) * 2, out NavMeshHit hit, 2, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        // Get facing direction
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, _item.transform.position.WithoutY() - destination.WithoutY());

        IMovement pickerMovement = picker.ComponentRepository.GetCachedComponent<IMovement>();
        pickerMovement.ProcedureMoveTo(destination, rotation, () =>
        {
            ItemPickup pickupItem = Instantiate(_item.ItemConfig.itemVisualPrefab, _item.transform.position, _item.transform.rotation);

            pickupItem.transform.DOJump(picker.GetPickupItemPositon(), 1, 1, .7f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                picker.AddItem(pickupItem.ItemConfig, pickupItem.transform);
                pickerMovement.EndProcedureMove();
                onInteractionDone?.Invoke();
            });



            Destroy(_item.gameObject);
        });

    }
}
