using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class ActivationMechanism : MonoBehaviour
    {
        public event Action OnPlatePressed;
        public event Action OnPlateReleased;
        
        protected void InvokeOnPlatePressed()
        {
            OnPlatePressed?.Invoke();
        }
        
        protected void InvokeOnPlateReleased()
        {
            OnPlateReleased?.Invoke();
        }
    }
}