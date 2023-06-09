using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionButton : MonoBehaviour
{
    [SerializeField] private Button _activeButton;
    [SerializeField] private Button _inactiveButton;

    private IInteracter _interacterSource;
    private IInteractable _interactableSource;
    private bool _isSetup;

    public bool ShouldShow { get; private set; } = false;

    public void Init(IInteracter interacter, IInteractable interactable, bool shouldShow)
    {
        if(!_isSetup)
        {
            _isSetup = true;

            _interactableSource = interactable;
            _interacterSource = interacter;

            Setup();
        }

        ShouldShow = shouldShow;

        if (shouldShow)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Setup()
    {
        _activeButton.onClick.AddListener(() => _interactableSource.OnInteract(_interacterSource));
    }

    public void SetInteractable(bool interactable)
    {
        _activeButton.gameObject.SetActive(interactable);
        _inactiveButton.gameObject.SetActive(!interactable);
    }

    public void AddListener()
    {

    }
}
