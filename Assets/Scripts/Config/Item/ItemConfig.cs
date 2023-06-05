namespace Nowhere.Item
{

    using Sirenix.OdinInspector;

    using UnityEngine;

    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Config/Item/Config")]
    public class ItemConfig : ScriptableObject
    {
        [HideInTables]
        [SerializeField] private SerializableGuid id;
        public SerializableGuid ID => id;

        [HideInTables]
        [AssetSelector(Paths = "Assets/Prefabs/Items")]
        public Item itemPrefab;

    }
}
