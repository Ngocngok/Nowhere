using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerInteractionButton : MonoBehaviour
{
    [SerializeField] private Button _activeButton;
    [SerializeField] private Button _inactiveButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _rectTransform;

    private PlayerInteractionPopup _interactionPopup;
    private IInteractable _interactable;
    private IInteractor _interacter;

    private bool _isSetup;

    public bool ShouldShow { get; private set; } = false;
    public CanvasGroup CanvasGroup => _canvasGroup;
    public RectTransform RectTransform => _rectTransform;


    public void Init(bool shouldShow, bool isVisuallyInteractable = true)
    {
        ShouldShow = shouldShow;

        if (isVisuallyInteractable)
        {
            _activeButton.gameObject.SetActive(true);
            _inactiveButton.gameObject.SetActive(false);
        }
        else
        {
            _activeButton.gameObject.SetActive(false);
            _inactiveButton.gameObject.SetActive(true);
        }
    }

    public void Setup(PlayerInteractionPopup interactionPopup, IInteractable interactable, IInteractor interacter)
    {
        if (!_isSetup)
        {
            _isSetup = true;

            _interactionPopup = interactionPopup;
            _interactable = interactable;
            _interacter = interacter;
        
            if (TryGetComponent(out OneClickButtonBehavior buttonBehavior))
            {
                _activeButton.onClick.AddListener(() => buttonBehavior.OnClick(_interactionPopup, _interactable, _interacter));
            }
        }

    }

    public void SetLogicallyInteractable(bool isInteractable)
    {
        _activeButton.interactable = isInteractable;
    }
}
