using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class RestartButtonUI : MonoBehaviour
    {
        [SerializeField, Self] private Button button;
    
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            GameManager.Instance.RestartLevel();
        }
    
        private void OnDisable()
        {
            button.onClick.RemoveListener(Restart);
        }
    }
}