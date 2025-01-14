using Animal;
using DG.Tweening;
using PolygonArsenal;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamehaSkill : MonoBehaviour
{
    [SerializeField] private ComponentRepository repository;
    [SerializeField] private ParticleSystem chargeFX;
    [SerializeField] private ParticleSystem burstFX;
    [SerializeField] private ParticleSystem aftermatchSmokeFX;
    [SerializeField] private PolygonBeamStatic skillFX;

    [Button]    
    public void CastSkill()
    {
        repository.GetCachedComponent<Animator>().SetTrigger(AnimationController.kamehaSkillHash);

        chargeFX.gameObject.SetActive(true);
        chargeFX.transform.localPosition = Vector3.zero;
        chargeFX.Play();

        chargeFX.transform.DOShakePosition(2.5f, .5f, 20, 50);
        chargeFX.transform.DOScale(15, 2.5f).OnComplete(() =>
        {
            chargeFX.transform.DOScale(0, .3f);

            DOVirtual.DelayedCall(.32f, () =>
            {
                burstFX.Play();
            });

            DOVirtual.DelayedCall(.35f, () =>
            {
                chargeFX.Stop();
                chargeFX.gameObject.SetActive(false);

                skillFX.gameObject.SetActive(true);
            });

            DOVirtual.DelayedCall(4.1f, () =>
            {
                if(skillFX.BeamEnd != null)
                {
                    ParticleSystem smoke = Instantiate(aftermatchSmokeFX, skillFX.BeamEnd.transform.position, Quaternion.identity);
                    
                    DOVirtual.DelayedCall(.7f, () =>
                    {
                        smoke.Play();
                    });

                    DOVirtual.DelayedCall(4, () =>
                    {
                        Destroy(smoke.gameObject);
                    });
                }
                skillFX.gameObject.SetActive(false);
            });
        });
    }
}
