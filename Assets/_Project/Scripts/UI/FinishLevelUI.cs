using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class FinishLevelUI : UI
    {
        [SerializeField] private Button mainMenuButton;
        
        private void Awake()
        {
            mainMenuButton.onClick.AddListener(GameManager.Instance.GoToMainMenu);
            mainMenuButton.onClick.AddListener(InvokeOnAnyButtonClicked);
        }
        
        private void OnDestroy()
        {
            mainMenuButton.onClick.RemoveAllListeners();
        }
    }
}