using System;
using System.Collections.Generic;
using _Project.Scripts.Behaviours;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class Torch : ActivationMechanism
    {
        [SerializeField, Self] private MeltingObject meltingObject;
        [SerializeField] private List<FreezingVisuals> freezingStages;
        [SerializeField] private GameObject visualPlaceholder;

        private IMeltingBehaviour _meltingMeltingBehaviour;
        private readonly string _torchActionName = "TorchLight";

        
        public event Action OnTorchLit;
        public static event Action OnAnyTorchLit;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Awake()
        {
            visualPlaceholder.SetActive(false);
            _meltingMeltingBehaviour =
                new ChangingVisualOnMeltingMeltingBehaviour(freezingStages, meltingObject, transform);
            _meltingMeltingBehaviour.Awake();
        }

        private void OnEnable()
        {
            meltingObject.OnTotallyMelted += LightTorch;
            _meltingMeltingBehaviour.OnEnable();
        }

        private void LightTorch()
        {
            OnTorchLit?.Invoke();
            OnAnyTorchLit?.Invoke();
        }

        private void OnDisable()
        {
            meltingObject.OnTotallyMelted -= LightTorch;
            _meltingMeltingBehaviour.OnDisable();
        }
    }
}