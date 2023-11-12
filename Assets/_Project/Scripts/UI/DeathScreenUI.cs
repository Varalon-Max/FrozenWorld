using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class DeathScreenUI : UI
    {
        [SerializeField] private Transform container;
        [SerializeField] private Button tryAgainButton;
        [SerializeField] private Button mainMenuButton;
        

        private void Start()
        {
            tryAgainButton.onClick.AddListener(Call);
            tryAgainButton.onClick.AddListener(InvokeOnAnyButtonClicked);
            mainMenuButton.onClick.AddListener(GameManager.Instance.GoToMainMenu);
            mainMenuButton.onClick.AddListener(InvokeOnAnyButtonClicked);
            container.gameObject.SetActive(false);
        }

        private void Call()
        {
            GameManager.Instance.RestartLevel();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnPlayerDead += PlayerDead;
        }

        private void PlayerDead()
        {
            container.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            GameManager.Instance.OnPlayerDead -= PlayerDead;
        }

        private void OnDestroy()
        {
            tryAgainButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();
        }
    }
}