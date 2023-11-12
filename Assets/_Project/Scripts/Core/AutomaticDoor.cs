﻿using UnityEngine;

namespace _Project.Scripts.Core
{
    public class AutomaticDoor: MonoBehaviour
    {
        [SerializeField] private ActivationMechanism activationMechanism;
        private readonly string _doorActionName = "OpenDoor";

        private void OnEnable()
        {
            activationMechanism.OnPlatePressed += OpenDoor;
        }

        private void OpenDoor()
        {
            // TODO: add door opening animation
            SoundManager.Instance.Play2DSound(_doorActionName);
            Destroy(gameObject);
        }
        
        private void OnDisable()
        {
            activationMechanism.OnPlatePressed -= OpenDoor;
        }
    }
}