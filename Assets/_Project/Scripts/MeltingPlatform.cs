using System;
using System.Collections.Generic;
using System.Linq;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class MeltingPlatform : MonoBehaviour
    {
        [SerializeField, Self] private MeltingObject meltingObject;
        [SerializeField] private List<FreezingVisuals> freezingStages;
        [SerializeField] private GameObject visualsPlaceholder;
        
        private GameObject _defaultVisualsPrefab;
        private GameObject _currentVisuals;

        private void OnValidate()
        {
            this.ValidateRefs();
            if (freezingStages.Count != 0)
            {
                _defaultVisualsPrefab = freezingStages.First(v => v.maxFreezingBound == 1).frozenVisualsPrefab;
                freezingStages = freezingStages.OrderBy(v => v.maxFreezingBound).ToList();
            }
        }

        private void OnEnable()
        {
            meltingObject.OnTotallyMelted += OnTotallyMelted;
            meltingObject.OnFreezingChanged += OnFreezingChanged;
        }

        private void Awake()
        {
            Destroy(visualsPlaceholder);
            ChangeVisuals(_defaultVisualsPrefab);
        }

        private void OnFreezingChanged(float currentFreezing)
        {
            foreach (var freezingStage in freezingStages)
            {
                if (currentFreezing <= freezingStage.maxFreezingBound)
                {
                    ChangeVisuals(freezingStage.frozenVisualsPrefab);
                    return;
                }
            }
        }

        private void ChangeVisuals(GameObject newVisualsPrefab)
        {
            if (_currentVisuals != null)
            {
                Destroy(_currentVisuals);
            }

            _currentVisuals = Instantiate(newVisualsPrefab, transform);
        }

        private void OnTotallyMelted()
        {
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            meltingObject.OnTotallyMelted -= OnTotallyMelted;
            meltingObject.OnFreezingChanged -= OnFreezingChanged;
        }
    }

    [Serializable]
    public struct FreezingVisuals
    {
        public GameObject frozenVisualsPrefab;
        public float maxFreezingBound;
    }
}