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

    private readonly Stack<GameObject> _holdItemInformation = new();
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

    public void AddItem(GameObject itemPickup, Transform itemTransform)
    {
        itemTransform.SetParent(_holdItemTransform[_itemCount], true);
        itemTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        itemTransform.localScale = Vector3.one;
        _holdItemInformation.Push(itemPickup);

        _itemCount++;
    }

    private GameObject RemoveItem(GameObject itemToRemove)
    {
        if (itemToRemove != null && _holdItemInformation.TryPeek(out GameObject item) && item == itemToRemove)
        {
            GameObject itemRemoved = _holdItemInformation.Pop();
            _itemCount--;
            itemToRemove.transform.SetParent(null);
            return itemRemoved;
        }
        return null;
    }

    public bool TryPeekTopItem(out GameObject item)
    {
        return _holdItemInformation.TryPeek(out item);
    }

    public GameObject PopTopItem()
    {
        if(!_holdItemInformation.TryPeek(out GameObject item))
        {
            return null;
        }

        return RemoveItem(item);
    }

    public Vector3 GetPickupItemPositon()
    {
        return _holdItemTransform[_itemCount].position;
    }
    
}
