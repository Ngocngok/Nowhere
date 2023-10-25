using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity (Pickupable)", menuName = "Config/Interaction/Pickupable")]
public class PickupableConfig : ScriptableObject
{
    [AssetSelector(Paths = "Assets/Prefabs/Entities/Pickups")]
    public EntityPickup entityVisualPrefab;
}
