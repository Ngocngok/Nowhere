using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparePart : MonoBehaviour
{
    public SparePartType partType;
    public SparePartAssembled sparePartAssembled;
}

public enum SparePartType
{
    PowerSource,
}
