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
        _characterDetection = _componentRepository.GetCachedComponent<CharacterDetection>();
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
            PlayerInteractionButton button = Instantiate(interaction.InteractionConfig.playerInteractionButton, _playerInteractionButtonLayout.transform, false);
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
            IInteractable interactable = interactionButton.Key;

            foreach (IInteracter interacter in interacters)
            {
                if (interactionButton.Key.IsCompatibleWithInteracter(interacter))
                {
                    interactionButton.Value.Setup(this, interactable, interacter);
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

        ShowAllActiveButton();
    }



    private void OnPlayerExit(CharacterBehavior character)
    {
        if (character is not PlayerBehavior)
        {
            return;
        }

        HideAllActiveButton();
    }

    public void ShowAllActiveButton(bool withAnimation = true)
    {
        _playerInteractionButtonLayout.Show(withAnimation, () => SetInteractableAllActiveButton(false), () => SetInteractableAllActiveButton(true));
    }

    public void HideAllActiveButton(bool withAnimation = true)
    {
        _playerInteractionButtonLayout.Hide(withAnimation, () => SetInteractableAllActiveButton(false), null);
    }

    private void SetInteractableAllActiveButton(bool isInteractable)
    {
        foreach (KeyValuePair<IInteractable, PlayerInteractionButton> button in _interactionButtonDictionary)
        {
            button.Value.SetLogicallyInteractable(isInteractable);
        }
    }
}
