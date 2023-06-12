using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionButtonBehavior
{
    void OnClick(PlayerInteractionPopup interactionPopup, IInteractable interactable, IInteracter interacter);
}
