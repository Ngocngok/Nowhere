using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Utilities;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;

public class PlayerInteractionButtonLayout : MonoBehaviour
{
    [SerializeField] private PlayerInteractionPopup _playerInteractionPopup;
    [SerializeField] private ButtonLayout _buttonLayout;

    [ShowIf("@_buttonLayout == ButtonLayout.Arc")]
    [SerializeField] private float _arcLayoutAngleRadian = Mathf.PI / 12;
    [ShowIf("@_buttonLayout == ButtonLayout.Arc")]
    [SerializeField] private float _arcLayoutRadius = 2;
    [ShowIf("@_buttonLayout == ButtonLayout.Arc")]
    [SerializeField] private float _arcLayoutOffsetY = 1.5f;

    [SerializeField] private float _animationDuration = 1f;

    private bool _withAnimation = true;

    public void Show(bool withAnimation, Action onShowAnimationStart, Action onShowAnimationDone)
    {
        _withAnimation = withAnimation;
        onShowAnimationStart?.Invoke();
        switch (_buttonLayout)
        {
            case ButtonLayout.Arc:
                ShowLayoutArc(onShowAnimationDone); 
                break;
            default: 
                break;
        }
    }

    public void Hide(bool withAnimation, Action onHideAnimationStart, Action onHideAnimationDone)
    {
        _withAnimation = withAnimation;
        onHideAnimationStart?.Invoke();
        switch (_buttonLayout)
        {
            case ButtonLayout.Arc:
                HideLayoutArc(onHideAnimationDone);
                break;
            default:
                break;
        }
    }

    private void ShowLayoutArc(Action onShowAnimationDone)
    {
        List<PlayerInteractionButton> _buttonList = new();
        _playerInteractionPopup.InteractionButtonDictionary.Where(button => button.Value.ShouldShow).ForEach(button => _buttonList.Add(button.Value));

        float angleOffset = Mathf.PI / 2 - _arcLayoutAngleRadian * (_buttonList.Count - 1) / 2;  

        for (int i = 0; i < _buttonList.Count; i++)
        {
            float x = Mathf.Cos(i * _arcLayoutAngleRadian + angleOffset) * _arcLayoutRadius;
            float y = Mathf.Sin(i * _arcLayoutAngleRadian + angleOffset) * _arcLayoutRadius - _arcLayoutOffsetY;

            _buttonList[i].gameObject.SetActive(true);
            _buttonList[i].CanvasGroup.alpha = 0;
            _buttonList[i].RectTransform.anchoredPosition = new Vector2(0, _arcLayoutRadius - _arcLayoutOffsetY);

            _buttonList[i].CanvasGroup.DOFade(1, _animationDuration).SetEase(Ease.InCubic);
            _buttonList[i].RectTransform.DOAnchorPosX(x, _animationDuration).SetEase(Ease.Linear);
            _buttonList[i].RectTransform.DOAnchorPosY(y, _animationDuration).SetEase(Ease.Linear);

            _buttonList[i].SetShowingState(true);
        }

        // Bla bla
        DOVirtual.DelayedCall(_animationDuration, () =>
        {
            onShowAnimationDone?.Invoke();
        });
    }

    private void HideLayoutArc(Action onHideAnimationDone)
    {
        List<PlayerInteractionButton> _buttonList = new();
        _playerInteractionPopup.InteractionButtonDictionary.Where(button => button.Value.IsBeingShown).ForEach(button => _buttonList.Add(button.Value));

        //float angleOffset = Mathf.PI / 4 - _arcLayoutAngleRadian * (_buttonList.Count - 1) / 2;

        float x = 0;
        float y = _arcLayoutRadius - _arcLayoutOffsetY;

        for (int i = 0; i < _buttonList.Count; i++)
        {
            int capturedIndex = i;
            _buttonList[i].RectTransform.DOAnchorPosX(x, _animationDuration).SetEase(Ease.Linear);
            _buttonList[i].RectTransform.DOAnchorPosY(y, _animationDuration).SetEase(Ease.Linear);
            _buttonList[i].CanvasGroup.DOFade(0, _animationDuration).SetEase(Ease.InQuint).OnComplete(() => _buttonList[capturedIndex].gameObject.SetActive(false));

            _buttonList[i].SetShowingState(false);
        }

        // Bla bla
        onHideAnimationDone?.Invoke();
    }

    private enum ButtonLayout
    {
        Verticle,
        Horizontal,
        Grid,
        Arc,
    }
}
