using System;
using _Project.Scripts.Core;
using UnityEngine;

namespace _Project.Scripts
{
    public class Level : MonoBehaviour
    {
        private int _totalTorches;
        private int _torchesActivated;

        public int TotalTorches => _totalTorches;
        public int TorchesActivated => _torchesActivated;

        public event Action OnAllTorchesActivated;  
        public event Action OnTorchActivated;

        private void OnEnable()
        {
            Torch.OnAnyTorchLit += TorchOnAnyTorchLit;
        }

        private void Awake()
        {
            _totalTorches = FindObjectsOfType<Torch>().Length;
        }

        private void TorchOnAnyTorchLit()
        {
            _torchesActivated++;
            OnTorchActivated?.Invoke();

            if (_torchesActivated>= _totalTorches)
            {
                OnAllTorchesActivated?.Invoke();
            }
        }

        private void OnDisable()
        {
            Torch.OnAnyTorchLit -= TorchOnAnyTorchLit;
        }
    }
}