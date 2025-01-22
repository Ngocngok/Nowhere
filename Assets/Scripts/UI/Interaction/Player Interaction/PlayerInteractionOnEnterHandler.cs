using Nowhere.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class PlayerInteractionOnEnterHandler : MonoBehaviour, IPlayerInteractionHandler
{
    private PlayerInteractionPopup _playerInteractionPopup;
    private ComponentRepository _componentRepository;
    private CharacterDetection _characterDetection;

    private Highlighter _highlighter;
    private CharacterBehavior _currentCharacterBehavior;

    public void Initialize(PlayerInteractionPopup popup)
    {
        _playerInteractionPopup = popup;
        _componentRepository = GetComponentInParent<ComponentRepository>();
        _characterDetection = _componentRepository.GetCachedComponent<CharacterDetection>();
        _highlighter = _componentRepository.GetCachedComponent<Highlighter>();

        _characterDetection.OnCharacterEnterObservable.Subscribe(OnPlayerEnterHandler);
        _characterDetection.OnCharacterExitObservable.Subscribe(OnPlayerExitHandler);
    }


    private void OnPlayerEnterHandler(CharacterBehavior character)
    {
        if (character is not PlayerBehavior)
        {
            return;
        }

        _currentCharacterBehavior = character;

        character.EventHandler.RegisterEvent(PlayerEvent.AfterPlayerInteract, OnAfterPlayerInteractHandler);

        SearchForInteraction(character);
    }


    private void OnAfterPlayerInteractHandler()
    {
        SearchForInteraction(_currentCharacterBehavior);
    }

    private void SearchForInteraction(CharacterBehavior character)
    {

        IInteractor[] interacters = character.GetComponent<ComponentRepository>().GetCachedComponents<IInteractor>();

        bool hasAtLeastOneInteraction = false;
        foreach (KeyValuePair<IInteractable, PlayerInteractionButton> interactionButton in _playerInteractionPopup.InteractionButtonDictionary)
        {
            bool foundCompatibleInteracter = false;
            IInteractable interactable = interactionButton.Key;

            foreach (IInteractor interacter in interacters)
            {
                if (interactionButton.Key.IsCompatibleWithInteractor(interacter))
                {
                    interactionButton.Value.Setup(_playerInteractionPopup, interactable, interacter);
                    interactionButton.Value.Init(true, interactable.IsInteractable(interacter));
                    foundCompatibleInteracter = true;
                    hasAtLeastOneInteraction = true;
                    break;
                }
            }

            if (!foundCompatibleInteracter)
            {
                interactionButton.Value.Init(false);
            }
        }

        if(hasAtLeastOneInteraction)
        {
            _highlighter.StartHighlight();
            _playerInteractionPopup.ShowAllActiveButton();
        }
        else
        {
            _highlighter.StopHighlight();
            _playerInteractionPopup.HideAllActiveButton();
        }

    }

    private void OnPlayerExitHandler(CharacterBehavior character)
    {
        if (character is not PlayerBehavior)
        {
            return;
        }
        
        character.EventHandler.UnregisterEvent(PlayerEvent.AfterPlayerInteract, OnAfterPlayerInteractHandler);
        _playerInteractionPopup.HideAllActiveButton();
        _highlighter.StopHighlight();
    }

    private void OnDisable()
    {
        if(_currentCharacterBehavior != null)
        {
            _currentCharacterBehavior.EventHandler.UnregisterEvent(PlayerEvent.AfterPlayerInteract, OnAfterPlayerInteractHandler);
        }
    }
}
