using System;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class AutomaticDoor: MonoBehaviour
    {
        [SerializeField] private ActivationMechanism activationMechanism;
        public static event Action OnDoorOpened;
        private void OnEnable()
        {
            activationMechanism.OnPlatePressed += OpenDoor;
        }

        private void OpenDoor()
        {
            // TODO: add door opening animation
            OnDoorOpened?.Invoke();
            Destroy(gameObject);
        }
        
        private void OnDisable()
        {
            activationMechanism.OnPlatePressed -= OpenDoor;
        }
    }
}