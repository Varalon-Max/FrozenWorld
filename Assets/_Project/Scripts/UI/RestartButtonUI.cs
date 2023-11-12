using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class RestartButtonUI : UI
    {
        [SerializeField, Self] private Button button;
    
        private void OnValidate()
        {
            this.ValidateRefs();
        }
        private void OnEnable()
        {
            button.onClick.AddListener(Restart);
            button.onClick.AddListener(InvokeOnAnyButtonClicked);
        }

        private void Restart()
        {
            GameManager.Instance.RestartLevel();
        }
    
        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}