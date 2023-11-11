using System;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts
{
    public class Torch : ActivationMechanism
    {
        [SerializeField, Self] private MeltingObject meltingObject;
        [SerializeField] private List<FreezingVisuals> freezingStages;
        [SerializeField] private GameObject visualPlaceholder;

        private IBehaviour _meltingBehaviour;
        
        public event Action OnTorchLit;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Awake()
        {
            visualPlaceholder.SetActive(false);
            _meltingBehaviour =
                new ChangingVisualOnMeltingBehaviour(freezingStages, meltingObject, transform);
            _meltingBehaviour.Awake();
        }

        private void OnEnable()
        {
            meltingObject.OnTotallyMelted += LightTorch;
            _meltingBehaviour.OnEnable();
        }

        private void LightTorch()
        {
            OnTorchLit?.Invoke();
        }

        private void OnDisable()
        {
            meltingObject.OnTotallyMelted -= LightTorch;
            _meltingBehaviour.OnDisable();
        }
    }
}