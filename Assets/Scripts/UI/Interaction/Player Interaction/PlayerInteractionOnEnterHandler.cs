using Nowhere.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerInteractionOnEnterHandler : MonoBehaviour, IPlayerInteractionHandler
{
    private PlayerInteractionPopup _playerInteractionPopup;
    private ComponentRepository _componentRepository;
    private CharacterDetection _characterDetection;

    public void Initialize(PlayerInteractionPopup popup)
    {
        _playerInteractionPopup = popup;
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _characterDetection = _componentRepository.GetCachedComponent<CharacterDetection>();


        _characterDetection.OnCharacterEnterObservable.Subscribe(OnStartInteraction);
        _characterDetection.OnCharacterExitObservable.Subscribe(OnStopInteraction);
    }


    private void OnStartInteraction(CharacterBehavior character)
    {
        if (character is not PlayerBehavior)
        {
            return;
        }

        IInteracter[] interacters = character.GetComponent<ComponentRepository>().GetCachedComponents<IInteracter>();

        foreach (KeyValuePair<IInteractable, PlayerInteractionButton> interactionButton in _playerInteractionPopup.InteractionButtonDictionary)
        {
            bool foundCompatibleInteracter = false;
            IInteractable interactable = interactionButton.Key;

            foreach (IInteracter interacter in interacters)
            {
                if (interactionButton.Key.IsCompatibleWithInteracter(interacter))
                {
                    interactionButton.Value.Setup(_playerInteractionPopup, interactable, interacter);
                    interactionButton.Value.Init(true, interactable.IsInteractable(interacter));
                    foundCompatibleInteracter = true;

                    break;
                }
            }

            if (!foundCompatibleInteracter)
            {
                interactionButton.Value.Init(false);
            }
        }

        _playerInteractionPopup.ShowAllActiveButton();
    }



    private void OnStopInteraction(CharacterBehavior character)
    {
        if (character is not PlayerBehavior)
        {
            return;
        }

        _playerInteractionPopup.HideAllActiveButton();
    }

}
