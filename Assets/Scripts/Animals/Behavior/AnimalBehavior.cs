using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventHandler))]
public class AnimalBehavior : MonoBehaviour
{
    [SerializeField] private EventHandler eventHandler;
    
    
    EventType<int> anEvent = new EventType<int>();

    EventType<int, int> anotherEvent = new EventType<int, int>();

    private void Awake()
    {

    }

    private void Update()
    {

    }

}
