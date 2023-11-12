using KBCore.Refs;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class LevelUI : UI
    {
        [SerializeField] private TMP_Text torchesText;
        [SerializeField, Child] private ThermometerUI thermometerUI;
        private Level _level;
        private Player.Player _player;
        
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Awake()
        {
            _player = FindObjectOfType<Player.Player>();
            _level = FindObjectOfType<Level>();
        }

        private void OnEnable()
        {
            _level.OnTorchActivated += OnTorchActivated;
        }

        private void Start()
        {
            UpdateText();
            _player.OnAccelerationCooldownChanged += thermometerUI.SetFillAmount;
            _player.OnAccelerationStart += thermometerUI.SetFillAmountToZero;
        }

        private void OnTorchActivated()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            torchesText.text = $"{_level.TorchesActivated} / {_level.TotalTorches}";
        }

        private void OnDisable()
        {
            _level.OnTorchActivated -= OnTorchActivated;
        }
        
        private void OnDestroy()
        {
            _player.OnAccelerationCooldownChanged -= thermometerUI.SetFillAmount;
            _player.OnAccelerationStart -= thermometerUI.SetFillAmountToZero;
        }
    }
}