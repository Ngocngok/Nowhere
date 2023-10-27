using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectable : MonoBehaviour, IPointerClickHandler
{
    private ComponentRepository _componentRepository;

    private void Awake()
    {
        _componentRepository = GetComponentInParent<ComponentRepository>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Oh no");
    }
}
