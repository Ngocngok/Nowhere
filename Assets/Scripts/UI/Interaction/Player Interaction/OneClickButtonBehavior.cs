using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OneClickButtonBehavior : MonoBehaviour, IInteractionButtonBehavior
{
    public void OnClick(PlayerInteractionPopup interactionPopup, IInteractable interactable, IInteractor interacter)
    {
        interactable.OnInteract(interacter, () => interactionPopup.HideAllActiveButton(), null);
    }
}
