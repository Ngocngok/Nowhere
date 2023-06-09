using Nowhere.Interaction;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static PlayerInteractionButtonLayout;

public class PlayerInteractionPopup : MonoBehaviour
{
    [SerializeField] private PlayerInteractionButtonLayout _playerInteractionButtonLayout;

    private ComponentRepository _componentRepository;
    private CharacterDetection _characterDetection;
    private IInteractable[] _playerInteractableList;
    private PlayerBehavior _playerBehavior;

    private readonly List<PlayerInteractionButton> _buttonList = new();
    public IReadOnlyList<PlayerInteractionButton> ButtonList => _buttonList;

    private void Awake()
    {
        _componentRepository= GetComponentInParent<ComponentRepository>();
        _characterDetection = _componentRepository.GetComponent<CharacterDetection>();
        _componentRepository.TryGetCachedComponents(out _playerInteractableList);
    }

    private void Start()
    {
        GenerateButton();

        _characterDetection.OnCharacterEnterObservable.Subscribe(OnPlayerEnter);
        _characterDetection.OnCharacterExitObservable.Subscribe(OnPlayerExit);
    }

    private void GenerateButton()
    {
        foreach (IInteractable playerInteraction in _playerInteractableList)
        {
            PlayerInteractionButton button = Instantiate(playerInteraction.InteractionConfig.playerInteractionButton, _playerInteractionButtonLayout.transform, true);
            button.gameObject.SetActive(false);
            _buttonList.Add(button);
        }
    }

    private void OnPlayerEnter(CharacterBehavior character)
    {
        if(character is not PlayerBehavior)
        {
            return;
        }

        if(_playerBehavior == null)
        {
            _playerBehavior = character as PlayerBehavior;
        }



    }

    private void OnPlayerExit(CharacterBehavior character)
    {
        if (character is not PlayerBehavior)
        {
            return;
        }
    }


}
