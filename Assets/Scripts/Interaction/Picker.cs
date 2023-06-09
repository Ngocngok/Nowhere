using Nowhere.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour, IInteracter
{
    [SerializeField] private Transform[] _holdItemTransform;

    public bool CanPickup => _itemCount < _maxItemCount;

    private readonly Stack<ItemConfig> _holdItemInformation = new();

    private int _itemCount = 0;
    private int _maxItemCount = 1;

    public void AddItem(ItemConfig itemConfig, Transform itemTransform)
    {
        itemTransform.SetParent(_holdItemTransform[_itemCount], true);
        _holdItemInformation.Push(itemConfig);

        _itemCount++;
    }

    public Vector3 GetPickupItemPositon()
    {
        return _holdItemTransform[_itemCount].position;
    }
    
}
