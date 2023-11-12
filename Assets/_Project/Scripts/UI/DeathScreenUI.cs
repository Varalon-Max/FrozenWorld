using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class DeathScreenUI : UI
    {
        [SerializeField] private Transform container;
        [SerializeField] private Button tryAgainButton;

        private void Start()
        {
            tryAgainButton.onClick.AddListener(GameManager.Instance.RestartLevel);
            tryAgainButton.onClick.AddListener(InvokeOnAnyButtonClicked);
            container.gameObject.SetActive(false);
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
        }
    }
}