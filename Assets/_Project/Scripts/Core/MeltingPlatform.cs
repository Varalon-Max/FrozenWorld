using System;
using System.Collections.Generic;
using _Project.Scripts.Behaviours;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class MeltingPlatform : MonoBehaviour
    {
        [SerializeField, Self] private MeltingObject meltingObject;
        [SerializeField] private List<FreezingVisuals> freezingStages;
        [SerializeField] private GameObject visualsPlaceholder;

        private GameObject _defaultVisualsPrefab;
        private GameObject _currentVisuals;

        private IMeltingBehaviour _meltingMeltingBehaviour;
        
        public static event Action OnAnyPlatformTotallyMelted;
        public static event Action OnAnyPlatformStateChanged;
        
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void OnEnable()
        {
            meltingObject.OnTotallyMelted += OnTotallyMelted;
            _meltingMeltingBehaviour.OnAnyStateChanged += OnAnyPlatformStateChanged;
            _meltingMeltingBehaviour.OnEnable();
        }

        private void Awake()
        {
            visualsPlaceholder.SetActive(false);
            _meltingMeltingBehaviour = new ChangingVisualOnMeltingMeltingBehaviour(freezingStages, meltingObject, transform);
            _meltingMeltingBehaviour.Awake();
        }

        private void OnTotallyMelted()
        {
            OnAnyPlatformTotallyMelted?.Invoke();
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            meltingObject.OnTotallyMelted -= OnTotallyMelted;
            _meltingMeltingBehaviour.OnAnyStateChanged -= OnAnyPlatformStateChanged;
            _meltingMeltingBehaviour.OnDisable();
        }
    }

    [Serializable]
    public struct FreezingVisuals
    {
        public GameObject frozenVisualsPrefab;
        public float maxFreezingBound;
    }
}