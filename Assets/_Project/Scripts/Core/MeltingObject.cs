using System;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class MeltingObject : MonoBehaviour
    {
        private const float MAX_FREEZING_TIME = 1f;

        [SerializeField, Self] private Collider2D platformCollider;
        [SerializeField] private float meltingSpeed = 0.2f;
        [SerializeField] private float freezingSpeed = 0.2f;

        private bool _isMelting;
        private HeatingArea _currentHeatingArea;
        private float _currentFreezingTime;
        private bool _isTotallyMelted;

        public event Action OnTotallyMelted;
        public event Action<float> OnFreezingChanged;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Awake()
        {
            _currentFreezingTime = MAX_FREEZING_TIME;
        }

        private void Update()
        {
            if (_isTotallyMelted)
            {
                return;
            }

            if (_isMelting)
            {
                _currentFreezingTime -= Time.deltaTime * meltingSpeed;

                if (_currentFreezingTime <= 0)
                {
                    _isTotallyMelted = true;
                    OnTotallyMelted?.Invoke();
                }
            }
            else
            {
                _currentFreezingTime += Time.deltaTime * freezingSpeed;
            }

            _currentFreezingTime = Mathf.Clamp(_currentFreezingTime, 0, MAX_FREEZING_TIME);
            OnFreezingChanged?.Invoke(_currentFreezingTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<HeatingArea>())
            {
                _isMelting = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<HeatingArea>())
            {
                _isMelting = false;
            }
        }
    }
}