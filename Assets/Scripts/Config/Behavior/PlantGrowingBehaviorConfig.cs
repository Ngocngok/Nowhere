using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity Growing (Behavior)", menuName = "Config/Behavior/Plant Growing")]
public class PlantGrowingBehaviorConfig : ScriptableObject
{
    public float[] scales;
    public GameObject[] treeGrowingStateModels;

}
