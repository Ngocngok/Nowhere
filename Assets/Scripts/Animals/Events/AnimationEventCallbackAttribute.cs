using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method)]
public class AnimationEventCallbackAttribute : Attribute
{
    public readonly string description = "This is an attribute for unity event method";
}
