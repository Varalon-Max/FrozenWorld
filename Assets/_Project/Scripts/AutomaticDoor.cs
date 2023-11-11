using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class AutomaticDoor: MonoBehaviour
    {
        [SerializeField] private ActivationMechanism activationMechanism;

        private void OnEnable()
        {
            activationMechanism.OnPlatePressed += OpenDoor;
        }

        private void OpenDoor()
        {
            throw new NotImplementedException();
        }
        
        private void OnDisable()
        {
            activationMechanism.OnPlatePressed -= OpenDoor;
        }
    }
}