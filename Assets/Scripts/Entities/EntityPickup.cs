using Nowhere.Item;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPickup : MonoBehaviour
{
    [Title("Config")]
    [SerializeField] private EntityConfig entityConfig;
    public EntityConfig EntityConfig => entityConfig;

}
