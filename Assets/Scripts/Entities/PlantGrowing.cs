using DG.Tweening;
using Nowhere.Item;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowing : MonoBehaviour
{
    [Title("Config")]
    [SerializeField] private EntityConfig entityConfig;
    public EntityConfig EntityConfig => entityConfig;



    [SerializeField] private float[] scale;
    [SerializeField] private float[] growTime;
    [SerializeField] private Transform visualTransform;

    private void Awake()
    {
        visualTransform.DOScale(scale[0], .8f).SetEase(Ease.InOutBack).SetDelay(.3f);
        DOVirtual.DelayedCall(2, () => StartGrowing());
    }


    public void StartGrowing()
    {
        Sequence growSequence = DOTween.Sequence();

        for (int i = 1; i < growTime.Length; i++)
        {
            int n = i;
            growSequence.Append(DOVirtual.DelayedCall(growTime[n], () =>
            {
                visualTransform.DOScale(scale[n], .8f).SetEase(Ease.InOutBack);
            }));
        }
    }

    public void OnRipe()
    {

    }

}
