using System;
using KBCore.Refs;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text torchesText;
        [SerializeField,Scene] private Level level;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void OnEnable()
        {
            level.OnTorchActivated += OnTorchActivated;
        }

        private void Start()
        {
            UpdateText();
        }

        private void OnTorchActivated()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            torchesText.text = $"{level.TorchesActivated} / {level.TotalTorches}";
        }

        private void OnDisable()
        {
            level.OnTorchActivated -= OnTorchActivated;
        }
    }
}