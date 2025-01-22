using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventHandler))]
public class CharacterBehavior : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;

    public EventHandler EventHandler => eventHandler;

    private void Awake()
    {

    }

    private void Update()
    {

    }

}
