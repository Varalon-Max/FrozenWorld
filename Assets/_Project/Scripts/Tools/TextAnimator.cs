using System.Collections.Generic;
using KBCore.Refs;
using MEC;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Tools
{
    public class TextAnimator : MonoBehaviour
    {
        [SerializeField, Self] private TextMeshProUGUI text;
        [SerializeField] private float animationSpeed = 0.2f;

        private string _finalText;
        private int _currentCharIndex;
        private char[] _chars;
        private bool _isPaused;
        private CoroutineHandle _animationCoroutine;


        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void Start()
        {
            Reset();
            StartAnimation();
        }

        private void Awake()
        {
            _finalText = text.text;
            _chars = _finalText.ToCharArray();
        }

        public void StartAnimation()
        {
            _animationCoroutine = Timing.RunCoroutine(AnimateTextCoroutine());
            _isPaused = false;
        }

        private IEnumerator<float> AnimateTextCoroutine()
        {
            while (_currentCharIndex < _chars.Length)
            {
                text.text += _chars[_currentCharIndex].ToString();
                _currentCharIndex++;
                yield return Timing.WaitForSeconds(animationSpeed);
            }
        }

        public void PauseAnimation()
        {
            if (!_isPaused)
            {
                Timing.PauseCoroutines(_animationCoroutine);
                _isPaused = true;
            }else
            {
                Timing.ResumeCoroutines(_animationCoroutine);
                _isPaused = false;
            }
        }

        public void Reset()
        {
            text.text = "";
            _currentCharIndex = 0;
        }

        public void FinishAnimation()
        {
            Timing.KillCoroutines(_animationCoroutine);
            text.text = _finalText;
            _isPaused = false;
        }
    }
}