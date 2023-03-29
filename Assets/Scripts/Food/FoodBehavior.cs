using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    [SerializeField] private PlayerDetection playerDetection;
    [SerializeField] private Highlighter highlighter;

    private void Reset()
    {
        playerDetection = GetComponentInChildren<PlayerDetection>();
        highlighter = GetComponentInChildren<Highlighter>();
    }

    private void OnEnable()
    {
        playerDetection.OnPlayerEnter += StartHighlight;
    }

    private void OnDisable()
    {
        playerDetection.OnPlayerExit += StopHighlight;
    }

    private void StartHighlight()
    {

    }

    private void StopHighlight()
    {

    }
}
