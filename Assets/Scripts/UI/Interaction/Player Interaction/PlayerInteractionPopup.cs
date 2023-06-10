using Nowhere.Interaction;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerInteractionPopup : MonoBehaviour
{
    [SerializeField] private PlayerInteractionButtonLayout _playerInteractionButtonLayout;

    private ComponentRepository _componentRepository;
    private CharacterDetection _characterDetection;
    private PlayerBehavior _playerBehavior;

    private readonly Dictionary<IInteractable, PlayerInteractionButton> _interactionButtonDictionary = new();
    public IReadOnlyDictionary<IInteractable, PlayerInteractionButton> InteractionButtonDictionary => _interactionButtonDictionary;

    private void Awake()
    {
        _componentRepository= GetComponentInParent<ComponentRepository>();
        _characterDetection = _componentRepository.GetComponent<CharacterDetection>();
    }

    private void Start()
    {
        GenerateButton();

        _characterDetection.OnCharacterEnterObservable.Subscribe(OnPlayerEnter);
        _characterDetection.OnCharacterExitObservable.Subscribe(OnPlayerExit);
    }

    private void GenerateButton()
    {
        foreach (IInteractable interaction in _componentRepository.GetCachedComponents<IInteractable>())
        {
            PlayerInteractionButton button = Instantiate(interaction.InteractionConfig.playerInteractionButton, _playerInteractionButtonLayout.transform, true);
            button.gameObject.SetActive(false);

            _interactionButtonDictionary.Add(interaction, button);
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

        IInteracter[] interacters = character.GetComponent<ComponentRepository>().GetCachedComponents<IInteracter>();

        foreach (KeyValuePair<IInteractable, PlayerInteractionButton> interactionButton in _interactionButtonDictionary)
        {
            bool foundCompatibleInteracter = false; 

            foreach (IInteracter interacter in interacters)
            {
                if (interactionButton.Key.IsCompatibleWithInteracter(interacter))
                {
                    interactionButton.Value.Init(true);
                    interactionButton.Value.ReplaceListener()
                    foundCompatibleInteracter |= true;

                    break;
                }
            }

            if (!foundCompatibleInteracter)
            {
                interactionButton.Value.Init(false);
            }
        }

        _playerInteractionButtonLayout.Show();
    }

    private void OnClickInteractionButton(IInteractable interactable)
    {
        if()
    }

    private void OnPlayerExit(CharacterBehavior character)
    {
        if (character is not PlayerBehavior)
        {
            return;
        }

        _playerInteractionButtonLayout.Hide();
    }


}
