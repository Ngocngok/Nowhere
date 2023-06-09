using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Nowhere.Interaction
{
    public class CharacterDetection : MonoBehaviour
    {
        public IObservable<CharacterBehavior> OnCharacterEnterObservable => _onCharacterEnterObservable;
        public IObservable<CharacterBehavior> OnCharacterExitObservable => _onCharacterExitObservable;

        private readonly Subject<CharacterBehavior> _onCharacterEnterObservable = new();
        private readonly Subject<CharacterBehavior> _onCharacterExitObservable = new();

        private readonly Dictionary<Collider, ColliderAssociatedInformation> _cachedCharacterColliders = new();

        private void OnTriggerEnter(Collider other)
        {
            // Check cache
            if (_cachedCharacterColliders.TryGetValue(other, out ColliderAssociatedInformation information))
            {
                information.OnCharacterDisabledSubscription.Disposable = information.CharacterBehavior.OnDisableAsObservable().FirstOrDefault().Subscribe(_ =>
                {
                    _onCharacterExitObservable.OnNext(information.CharacterBehavior);
                });

                _onCharacterEnterObservable.OnNext(information.CharacterBehavior);
                return;
            }

            // Check is character
            if (other.TryGetComponent(out CharacterBehavior characterBehavior))
            {
                ColliderAssociatedInformation newInformation;
                newInformation.CharacterBehavior = characterBehavior;
                newInformation.OnCharacterDisabledSubscription = new SerialDisposable();

                _cachedCharacterColliders.Add(other, newInformation);

                newInformation.OnCharacterDisabledSubscription.Disposable = characterBehavior.OnDisableAsObservable().FirstOrDefault().Subscribe(_ =>
                {
                    _onCharacterExitObservable.OnNext(characterBehavior);
                });

                _onCharacterEnterObservable.OnNext(characterBehavior);
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_cachedCharacterColliders.TryGetValue(other, out ColliderAssociatedInformation value))
            {
                _onCharacterExitObservable.OnNext(value.CharacterBehavior);
                value.OnCharacterDisabledSubscription.Disposable = null;
                return;
            }
        }

        private void OnDisable()
        {
            foreach (KeyValuePair<Collider, ColliderAssociatedInformation> colliderAssociatedInformation in _cachedCharacterColliders)
            {
                colliderAssociatedInformation.Value.OnCharacterDisabledSubscription.Disposable = null;
            }
        }

        private void OnDestroy()
        {
            foreach (KeyValuePair<Collider, ColliderAssociatedInformation> colliderAssociatedInformation in _cachedCharacterColliders)
            {
                colliderAssociatedInformation.Value.OnCharacterDisabledSubscription.Dispose();
            }
        }

        private struct ColliderAssociatedInformation
        {
            public CharacterBehavior CharacterBehavior;
            public SerialDisposable OnCharacterDisabledSubscription;
        }
    }
}