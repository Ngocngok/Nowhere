using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Nowhere.Interaction
{

    [CreateAssetMenu(fileName = "InteractionConfig", menuName = "Config/Interaction/Config")]
    public class InteractionConfig : ScriptableObject
    {
        [HideInTables]
        [SerializeField] private SerializableGuid id;
        public SerializableGuid ID => id;

        [HideInTables]
        [AssetSelector(Paths = "Assets/Prefabs/UI/UI (World Space)")]
        public PlayerInteractionButton playerInteractionButton;


    }
}
