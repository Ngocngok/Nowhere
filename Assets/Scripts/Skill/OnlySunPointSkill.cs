using Animal;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlySunPointSkill : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform onlySunPointTransform;
    [SerializeField] private ComponentRepository repository;

    [Button]
    public void CastSkill() 
    {
        repository.GetCachedComponent<Animator>().SetTrigger("skill_only_sun_point");

        onlySunPointTransform.position = target.position;

        Vector3 targetPosition = onlySunPointTransform.position;
        onlySunPointTransform.position += Vector3.up * 3.4f;
        onlySunPointTransform.localScale = Vector3.zero;
        onlySunPointTransform.gameObject.SetActive(true);

        onlySunPointTransform.DOScale(1, 3);
        onlySunPointTransform.DOShakePosition(3, .5f, 30, 10);

        DOVirtual.DelayedCall(3f, () =>
        {
            onlySunPointTransform.DOMove(targetPosition, .35f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1, () =>
                {
                    onlySunPointTransform.gameObject.SetActive(false);
                });
            });
        });

        DOVirtual.DelayedCall(3.1f, () =>
        {
            target.DOScaleY(0, .2f);
        });

    }
}
