using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Behaviours
{
    public class ChangingVisualOnMeltingMeltingBehaviour : IMeltingBehaviour
    {
        private MeltingObject _meltingObject;
        private List<FreezingVisuals> _freezingStages;
        private Transform _transform;

        private GameObject _defaultVisualsPrefab;
        private GameObject _currentVisuals;
        

        public ChangingVisualOnMeltingMeltingBehaviour(
            List<FreezingVisuals> freezingStages,
            MeltingObject meltingObject, 
            Transform transform)
        {
            _freezingStages = freezingStages;
            _meltingObject = meltingObject;
            _transform = transform;
        }

        public void OnEnable()
        {
            _meltingObject.OnFreezingChanged += OnFreezingChanged;
        }

        public void Awake()
        {
            if (_freezingStages.Count != 0)
            {
                _previousState = _freezingStages.Count - 1;
                _defaultVisualsPrefab = _freezingStages.First(v => v.maxFreezingBound == 1).frozenVisualsPrefab;
                _freezingStages = _freezingStages.OrderBy(v => v.maxFreezingBound).ToList();
            }

            ChangeVisuals(_defaultVisualsPrefab);
        }

        private void OnFreezingChanged(float currentFreezing)
        {
            for (var i = 0; i < _freezingStages.Count; i++)
            {
                var freezingStage = _freezingStages[i];
                if (currentFreezing <= freezingStage.maxFreezingBound)
                {
                    if (_previousState != i)
                    {
                        _previousState = i;
                        IMeltingBehaviour.InvokeOnAnyStateChanged();
                    }
                    ChangeVisuals(freezingStage.frozenVisualsPrefab);
                    return;
                }
            }
        }
        
        private int _previousState;

        private void ChangeVisuals(GameObject newVisualsPrefab)
        {
            if (_currentVisuals != null)
            {
                Object.Destroy(_currentVisuals);
            }

            _currentVisuals = Object.Instantiate(newVisualsPrefab, _transform);
        }

        public void OnDisable()
        {
            _meltingObject.OnFreezingChanged -= OnFreezingChanged;
        }
    }
}