using Sirenix.OdinInspector;
using UnityEngine;

namespace Nowhere.Item
{

    [CreateAssetMenu(fileName = "EntityConfig", menuName = "Config/Entity/Config")]
    public class EntityConfig : ScriptableObject
    {
        [HideInTables]
        [SerializeField] private SerializableGuid id;
        public SerializableGuid ID => id;

        [HideInTables]
        [AssetSelector(Paths = "Assets/Prefabs/Entities")]
        public Entity entityPrefab;
    }
}
