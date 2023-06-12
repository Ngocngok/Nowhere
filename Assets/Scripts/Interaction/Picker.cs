using Nowhere.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour, IInteracter
{
    private ComponentRepository _componentRepository;
    public ComponentRepository ComponentRepository => _componentRepository;

    [SerializeField] private Transform _rootItemHolder;
    private Transform[] _holdItemTransform;

    private readonly Stack<ItemConfig> _holdItemInformation = new();
    private int _itemCount = 0;
    private readonly int _maxItemCount = 2;

    public bool CanPickup => _itemCount < _maxItemCount;

    private void Awake()
    {
        _holdItemTransform = new Transform[_rootItemHolder.childCount];
        for (int i = 0; i < _holdItemTransform.Length; i++)
        {
            _holdItemTransform[i] = _rootItemHolder.GetChild(i);
        }

        _componentRepository = GetComponentInParent<ComponentRepository>();

    }

    public void AddItem(ItemConfig itemConfig, Transform itemTransform)
    {
        itemTransform.SetParent(_holdItemTransform[_itemCount], true);
        itemTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        itemTransform.localScale = Vector3.one;
        _holdItemInformation.Push(itemConfig);

        _itemCount++;
    }

    public Vector3 GetPickupItemPositon()
    {
        return _holdItemTransform[_itemCount].position;
    }
    
}
