namespace Nowhere.Item
{
    using Sirenix.OdinInspector;
    using UnityEngine;


    public class Item : MonoBehaviour
    {
        [Title("Config")]
        [SerializeField] private ItemConfig itemConfig;
        public ItemConfig ItemConfig => itemConfig;

        private bool isInited = false;

        private void Awake()
        {

        }

        public void Init()
        {
            if (isInited)
            {
                return;
            }

            isInited = true;
            //bla
        }



    }



}