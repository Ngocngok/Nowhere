using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity (Plantable)", menuName = "Config/Interaction/Plantable")]
public class PlantableConfig : ScriptableObject
{
    [AssetSelector(Paths = "Assets/Prefabs/Entities/PlantGrowings")]
    public PlantGrowing plantGrowingPrefab;
}
