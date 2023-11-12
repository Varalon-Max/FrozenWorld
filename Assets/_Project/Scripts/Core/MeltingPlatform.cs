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

        private IBehaviour _meltingBehaviour;
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void OnEnable()
        {
            meltingObject.OnTotallyMelted += OnTotallyMelted;
            _meltingBehaviour.OnEnable();
        }

        private void Awake()
        {
            visualsPlaceholder.SetActive(false);
            _meltingBehaviour = new ChangingVisualOnMeltingBehaviour(freezingStages, meltingObject, transform);
            _meltingBehaviour.Awake();
        }

        private void OnTotallyMelted()
        {
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            meltingObject.OnTotallyMelted -= OnTotallyMelted;
            _meltingBehaviour.OnDisable();
        }
    }

    [Serializable]
    public struct FreezingVisuals
    {
        public GameObject frozenVisualsPrefab;
        public float maxFreezingBound;
    }
}