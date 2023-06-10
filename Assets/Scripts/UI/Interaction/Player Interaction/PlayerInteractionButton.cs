using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerInteractionButton : MonoBehaviour
{
    [SerializeField] private Button _activeButton;
    [SerializeField] private Button _inactiveButton;

    private bool _isSetup;

    public bool ShouldShow { get; private set; } = false;

    public void Init(bool shouldShow)
    {
        if(!_isSetup)
        {
            _isSetup = true;

            Setup();
        }

        ShouldShow = shouldShow;
    }

    private void Setup()
    {

    }

    public void ReplaceListener(Action OnClick)
    {
        _activeButton.onClick.RemoveAllListeners();
        _activeButton.onClick.AddListener(() => OnClick?.Invoke());
    }

    public void SetInteractable(bool interactable)
    {
        _activeButton.gameObject.SetActive(interactable);
        _inactiveButton.gameObject.SetActive(!interactable);
    }
}
