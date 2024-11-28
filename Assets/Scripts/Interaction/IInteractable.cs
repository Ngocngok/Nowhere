using Nowhere.Interaction;
using System;

public interface IInteractable
{
    InteractionConfig InteractionConfig { get; }
    bool IsCompatibleWithInteractor(IInteractor interacter);
    bool IsInteractable(IInteractor interacter);
    void OnInteract(IInteractor interacter, Action onInteractionStart, Action onInteractionDone);
}

