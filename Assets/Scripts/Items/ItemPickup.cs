using Nowhere.Item;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Title("Config")]
    [SerializeField] private ItemConfig itemConfig;
    public ItemConfig ItemConfig => itemConfig;

}
