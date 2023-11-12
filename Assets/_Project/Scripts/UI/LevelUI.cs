using KBCore.Refs;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class LevelUI : UI
    {
        [SerializeField] private TMP_Text torchesText;
        [SerializeField,Scene] private Level level;
        [SerializeField, Child] private ThermometerUI thermometerUI;
        [SerializeField] private Player.Player player;
        
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
            player.OnAccelerationCooldownChanged += thermometerUI.SetFillAmount;
            player.OnAccelerationStart += thermometerUI.SetFillAmountToZero;
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
        
        private void OnDestroy()
        {
            player.OnAccelerationCooldownChanged -= thermometerUI.SetFillAmount;
            player.OnAccelerationStart -= thermometerUI.SetFillAmountToZero;
        }
    }
}