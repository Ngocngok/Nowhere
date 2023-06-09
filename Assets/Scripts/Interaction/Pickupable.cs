using DG.Tweening;
using Nowhere.Interaction;
using Nowhere.Item;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

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

    public bool IsInteractable(IInteracter interacter)
    {
        Picker picker = interacter as Picker;
        return picker.CanPickup;
    }

    public void OnInteract(IInteracter interacter)
    {
        Picker picker = interacter as Picker;

        ItemPickup pickupItem = Instantiate(_item.ItemConfig.itemVisualPrefab, _item.transform.position, _item.transform.rotation);
        pickupItem.transform.DOJump(picker.GetPickupItemPositon(), 5, 1, .5f).OnComplete(() =>
        {
            picker.AddItem(pickupItem.ItemConfig, pickupItem.transform);
        });

        Destroy(_item.gameObject);
    }
}
