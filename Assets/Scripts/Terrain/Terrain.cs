using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainType terrainType;

    public TerrainType TerrainType => terrainType;
}

public enum TerrainType
{
    Land,
    Water,
}
