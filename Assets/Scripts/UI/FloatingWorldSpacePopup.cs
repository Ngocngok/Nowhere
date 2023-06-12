using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingWorldSpacePopup : MonoBehaviour
{
    private void Awake()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public void Init()
    {

    }
}
