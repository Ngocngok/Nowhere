using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nowhere.Item
{

    [CreateAssetMenu(fileName = "EntityConfigGroup", menuName = "Config/Entity/ConfigGroup")]
    public class EntityConfigGroup : ScriptableObject
    {
        [TabGroup("Group1", "Entity Configs")]
        [TableList]
        public EntityConfig[] entityConfigs;

        private Dictionary<SerializableGuid, EntityConfig> _entityConfigLookup;

        public void Initialize()
        {
            _entityConfigLookup = entityConfigs.ToDictionary(entity => entity.ID);
        }

        public bool TryFindEntityByID(SerializableGuid id, out EntityConfig entityConfig)
        {
            return _entityConfigLookup.TryGetValue(id, out entityConfig);
        }

#if UNITY_EDITOR
        //[TabGroup("Main/Collection/Sub", "All")]
        //[Space]
        //[FolderPath(RequireExistingPath = true)]
        //[SerializeField]
        //private string itemLoadPath;

        //[TabGroup("Main/Collection/Sub", "All")]
        //[Button]
        //private void CollectItems()
        //{
        //    if (string.IsNullOrEmpty(itemLoadPath))
        //    {
        //        itemLoadPath = ConfigHelper.GetContainingFolder(this);
        //    }

        //    string[] fileEntries = Directory.GetFiles(itemLoadPath, "*.asset", SearchOption.AllDirectories);

        //    List<ItemConfig> configs = new List<ItemConfig>(fileEntries.Length);

        //    for (int i = 0; i < fileEntries.Length; i++)
        //    {
        //        string fileEntry = fileEntries[i];

        //        ItemConfig config = AssetDatabase.LoadAssetAtPath<ItemConfig>(fileEntry);

        //        if (config != null)
        //        {
        //            configs.Add(config);
        //        }
        //    }

        //    itemConfigs = configs.ToArray();
        //}

        //[TabGroup("Main/Collection/Sub", "All")]
        //[Button]
        //private void ValidateItems()
        //{
        //    Dictionary<SerializableGuid, ItemConfig> itemConfigLookup = new();
        //    foreach (ItemConfig itemConfig in itemConfigs)
        //    {
        //        if (itemConfig.ID.IsEmpty())
        //        {
        //            Debug.LogError($"ItemConfig {itemConfig.name} has empty ID", itemConfig);
        //            continue;
        //        }

        //        if (itemConfigLookup.TryGetValue(itemConfig.ID, out ItemConfig existedItemConfig))
        //        {
        //            Debug.LogError($"ItemConfig {itemConfig.name}'s ID has been associated with Config ({existedItemConfig.name})", itemConfig);
        //            continue;
        //        }

        //        itemConfigLookup.Add(itemConfig.ID, itemConfig);
        //    }
        //}

        //[TabGroup("Main/Collection/Sub", "Weapon")]
        //[PropertySpace]
        //[Button(Name = "Collect Items")]
        //private void CollectWeapons()
        //{
        //    weaponItems = itemConfigs
        //        .Where(itemConfig => itemConfig.tag == ItemTag.Weapon)
        //        .ToArray();
        //}
#endif
    }
}
