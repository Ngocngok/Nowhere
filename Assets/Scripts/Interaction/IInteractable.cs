using Nowhere.Interaction;
using System;

public interface IInteractable
{
    InteractionConfig InteractionConfig { get; }
    bool IsCompatibleWithInteracter(IInteracter interacter);
    bool IsInteractable(IInteracter interacter);
    void OnInteract(IInteracter interacter, Action onInteractionStart, Action onInteractionDone);
}

