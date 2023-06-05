namespace Nowhere.Item
{
    using UnityEngine;


    public class Item : MonoBehaviour
    {
        [SerializeField] private PlayerDetection playerDetection;
        [SerializeField] private Highlighter highlighter;

        private bool isInited = false;

        public void Init()
        {
            if (isInited)
            {
                return;
            }

            isInited = true;
            //bla
        }

        private void Reset()
        {
            playerDetection = GetComponentInChildren<PlayerDetection>();
            highlighter = GetComponentInChildren<Highlighter>();
        }

        private void OnEnable()
        {
            playerDetection.OnPlayerEnter += highlighter.StartHighlight;
            playerDetection.OnPlayerExit += highlighter.StopHighlight;
        }

        private void OnDisable()
        {
            playerDetection.OnPlayerEnter -= highlighter.StartHighlight;
            playerDetection.OnPlayerExit -= highlighter.StopHighlight;
        }

    }



}