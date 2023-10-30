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
    private IPlayerInteractionHandler _playerInteractionHandler;


    private readonly Dictionary<IInteractable, PlayerInteractionButton> _interactionButtonDictionary = new();
    public IReadOnlyDictionary<IInteractable, PlayerInteractionButton> InteractionButtonDictionary => _interactionButtonDictionary;

    private void Awake()
    {
        _componentRepository= GetComponentInParent<ComponentRepository>();
        _characterDetection = _componentRepository.GetCachedComponent<CharacterDetection>();
        _playerInteractionHandler = _componentRepository.GetCachedComponent<IPlayerInteractionHandler>();
    }

    private void Start()
    {
        GenerateButton();

        _playerInteractionHandler.Initialize(this);

        //_characterDetection.OnCharacterEnterObservable.Subscribe(OnStartInteraction);
        //_characterDetection.OnCharacterExitObservable.Subscribe(OnStopInteraction);
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
