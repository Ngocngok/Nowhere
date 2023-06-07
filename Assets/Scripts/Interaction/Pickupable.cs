using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour, IInteractable
{
    private PlayerDetection playerDetection;

    private void Awake()
    {
        playerDetection = GetComponent<PlayerDetection>();
    }

    public void Interact()
    {

    }
}
