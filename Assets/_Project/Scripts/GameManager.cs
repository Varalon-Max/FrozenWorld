using System;
using _Project.Scripts.Tools;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Material iceMaterial;
        private static readonly int Sides = Shader.PropertyToID("_Sides");
        private static readonly int Emission = Shader.PropertyToID("_Emission");
        public Material IceMaterial => iceMaterial;

        public static GameManager Instance { get; private set; }
        public event Action OnPlayerDead;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RestartLevel()
        {
            Debug.Log("Restarting level...");
            Loader.RestartScene();
        }

        public void HandlePlayerDead()
        {
            OnPlayerDead?.Invoke();
        }
    }
}