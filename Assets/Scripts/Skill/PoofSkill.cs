using Animal;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofSkill : MonoBehaviour
{
    [SerializeField] private ParticleSystem poofFx;
    [SerializeField] private GameObject camouflage;
    [SerializeField] private ComponentRepository componentRepository;

    private bool _camouflageOn = false;

    [Button]
    public void ToggleCamouflage()
    {
        if(!_camouflageOn)
        {
            componentRepository.GetCachedComponent<Animator>().SetTrigger(AnimationController.poofSkillHash);
            DOVirtual.DelayedCall(1f, () =>
            {
                _camouflageOn = !_camouflageOn;

                poofFx.Play();
                DOVirtual.DelayedCall(.2f, () =>
                {
                    camouflage.SetActive(_camouflageOn);
                });
            });
        }
        else
        {
            _camouflageOn = !_camouflageOn;

            poofFx.Play();
            DOVirtual.DelayedCall(.2f, () =>
            {
                camouflage.SetActive(_camouflageOn);
            });
        }

    }
}
