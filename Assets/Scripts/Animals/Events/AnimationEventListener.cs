using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    [AnimationEventCallback]
    public void InvokeEvent(string name)
    {
        Debug.Log(name);
    }


}
