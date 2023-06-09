using Sirenix.OdinInspector;
using UnityEngine;

namespace Nowhere.Item
{

    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Config/Item/Config")]
    public class ItemConfig : ScriptableObject
    {
        [HideInTables]
        [SerializeField] private SerializableGuid id;
        public SerializableGuid ID => id;

        [HideInTables]
        [AssetSelector(Paths = "Assets/Prefabs/Items/Items")]
        public Item itemPrefab;

        [HideInTables]
        [AssetSelector(Paths = "Assets/Prefabs/Items/ItemPickups")]
        public ItemPickup itemVisualPrefab;

    }
}
