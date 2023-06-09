using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionButtonLayout : MonoBehaviour
{
    [SerializeField] private ButtonLayout _buttonLayout;

    private List<PlayerInteractionButton> _buttonList;

    public void Init(List<PlayerInteractionButton> buttonList)
    {
        _buttonList = buttonList;
    }

    public void Show()
    {

    }

    public void Hide()
    {

    }

    private enum ButtonLayout
    {
        Verticle,
        Horizontal,
        Grid,
        Arc,
    }
}
