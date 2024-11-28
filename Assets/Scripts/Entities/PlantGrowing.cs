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

    [SerializeField] private PlantGrowingBehaviorConfig plantGrowingBehaviorConfig;


    [SerializeField] private Transform visualTransform;

    private int growingState = -1;


    private void Awake()
    {
        visualTransform.DOScale(Vector3.zero, .8f).SetEase(Ease.InOutBack).SetDelay(.3f);
        DOVirtual.DelayedCall(2, () => StartGrowing());
    }


    public void StartGrowing()
    {
        NextGrowingState();
    }

    [Button]
    public void NextGrowingState()
    {
        growingState++;

        Sequence changeGrowingStateSequence = DOTween.Sequence();

        changeGrowingStateSequence.Append(visualTransform.DOScale(Vector3.zero, .4f).SetEase(Ease.InOutBack));
        changeGrowingStateSequence.AppendCallback(() => Destroy(visualTransform.GetChild(0).gameObject));
        changeGrowingStateSequence.AppendCallback(() => 
        {
            GameObject treeModelNextState = Instantiate(plantGrowingBehaviorConfig.treeGrowingStateModels[growingState], visualTransform);
            treeModelNextState.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        });
        changeGrowingStateSequence.Append(visualTransform.DOScale(plantGrowingBehaviorConfig.scales[growingState], .8f).SetEase(Ease.InOutBack));

        
    }

    public void OnRipe()
    {

    }

}
