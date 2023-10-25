using Nowhere.Item;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [Title("Config")]
    [SerializeField] private PlantableConfig plantableConfig;
    public PlantableConfig PlantableConfig => plantableConfig;
}
