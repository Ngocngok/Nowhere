using MoreMountains.Tools;
using Nowhere.Utility;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private AVeryBigStructIDunno[] structs;
    public int size;

    private void Awake()
    {
        structs = new AVeryBigStructIDunno[size];
        for (int i = 0; i < size; i++) 
        {
            structs[i] = new AVeryBigStructIDunno();
            structs[i].id = i;
        }
    }

    [Button]
    private void Shuffle()
    {
        structs.MMShuffle();
    }

    [Button]
    private void SortRerach()
    {
        Array.Sort( structs, (x, y) => x.id - y.id);
    }

    AVeryBigStructIDunnoComparer comparer;

    [Button]
    private void SortPro1()
    {

        QuickSelect2.SmallestK(structs, structs.Length, 0, comparer);
    }

}

public struct AVeryBigStructIDunnoComparer : IComparer<AVeryBigStructIDunno>
{
    public readonly int Compare(AVeryBigStructIDunno x, AVeryBigStructIDunno y)
    {
        return x.id.CompareTo(y.id);
    }
}

public struct AVeryBigStructIDunno
{
    public int id;
    RaycastHit hit1;
    RaycastHit hit2;
    RaycastHit hit3;
    RaycastHit hit4;
    RaycastHit hit5;
}
