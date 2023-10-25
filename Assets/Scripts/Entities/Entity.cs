namespace Nowhere.Item
{
    using Sirenix.OdinInspector;
    using UnityEngine;


    public class Entity : MonoBehaviour
    {
        [Title("Config")]
        [SerializeField] private EntityConfig entityConfig;
        public EntityConfig EntityConfig => entityConfig;
    }
}