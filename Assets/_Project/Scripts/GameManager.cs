using System;
using _Project.Scripts.Tools;
using Eflatun.SceneReference;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Material iceMaterial;
        [SerializeField] private SceneReference mainScene;
        
        private static readonly int Sides = Shader.PropertyToID("_Sides");
        private static readonly int Emission = Shader.PropertyToID("_Emission");
        public Material IceMaterial => iceMaterial;

        public static GameManager Instance { get; private set; }
        public event Action OnPlayerDead;

        private void Awake()
        {
            if (Instance == this)
            {
                return;
            }
            
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
        }

        public void RestartLevel()
        {
            Loader.RestartScene();
        }

        public void HandlePlayerDead()
        {
            OnPlayerDead?.Invoke();
        }

        public void GoToMainMenu()
        {
            Loader.Load(mainScene);
        }
    }
}