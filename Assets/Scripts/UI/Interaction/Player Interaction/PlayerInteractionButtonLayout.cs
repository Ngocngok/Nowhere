using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.Utilities;
using Sirenix.OdinInspector;

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

    public void Show()
    {
        switch (_buttonLayout)
        {
            case ButtonLayout.Arc:
                ShowLayoutArc(); 
                break;
            default: 
                break;
        }
    }

    public void Hide()
    {

    }

    private void ShowLayoutArc()
    {
        List<PlayerInteractionButton> _buttonList = new();
        _playerInteractionPopup.InteractionButtonDictionary.Where(button => button.Value.ShouldShow).ForEach(button => _buttonList.Add(button.Value));

        float angleOffset = Mathf.PI / 4 - _arcLayoutAngleRadian * (_buttonList.Count - 1) / 2;  

        for (int i = 0; i < _buttonList.Count; i++)
        {
            float x = Mathf.Cos(i * _arcLayoutAngleRadian + angleOffset) * _arcLayoutRadius;
            float y = Mathf.Sin(i * _arcLayoutAngleRadian + angleOffset) * _arcLayoutRadius - _arcLayoutOffsetY;
        }
    }

    private enum ButtonLayout
    {
        Verticle,
        Horizontal,
        Grid,
        Arc,
    }
}
