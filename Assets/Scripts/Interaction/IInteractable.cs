using Nowhere.Interaction;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IInteractable
{
    InteractionConfig InteractionConfig { get; }
    bool IsInteractable(IInteracter interacter);
    void OnInteract(IInteracter interacter);
}
