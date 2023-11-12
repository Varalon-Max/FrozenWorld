using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        [SerializeField] private Sound[] musicSound;
        [SerializeField] private Sound[] sfxSound;
        [SerializeField] private AudioSource audioSourceSFX;
        [SerializeField] private AudioSource audioSourceMusic;


        private float _volume = 1f;

        private Dictionary<SoundPurpose, AudioClip> _sfxDictionary;

        private void Awake()
        {
            GrabSingleton();

            _sfxDictionary = sfxSound.ToDictionary(x => x.Purpose, x => x.AudioClip);
        }

        private void Start()
        {
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();
        }

        private void OnEnable()
        {
            AutomaticDoor.OnDoorOpened += OnDoorOpened;
            FinishDoor.OnDoorOpened += OnDoorOpened;
            MeltingPlatform.OnAnyPlatformTotallyMelted += OnAnyPlatformTotallyMelted;
            MeltingPlatform.OnAnyPlatformStateChanged += OnAnyPlatformStateChanged;
            MoveController.OnAnyJumpStarted += OnAnyJumpStarted;
            Torch.OnAnyTorchLit += OnAnyTorchLit;
            PressPlate.OnPlatePressed += OnAnyPlatePressed;
            UI.UI.OnAnyButtonClicked += OnAnyButtonClicked;
        }

        private void OnDisable()
        {
            AutomaticDoor.OnDoorOpened -= OnDoorOpened;
            FinishDoor.OnDoorOpened -= OnDoorOpened;
            MeltingPlatform.OnAnyPlatformTotallyMelted -= OnAnyPlatformTotallyMelted;
            MeltingPlatform.OnAnyPlatformStateChanged -= OnAnyPlatformStateChanged;
            MoveController.OnAnyJumpStarted -= OnAnyJumpStarted;
            Torch.OnAnyTorchLit -= OnAnyTorchLit;
            PressPlate.OnPlatePressed -= OnAnyPlatePressed;
            UI.UI.OnAnyButtonClicked -= OnAnyButtonClicked;
        }

        private void GrabSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayBackgroundNoise()
        {
            audioSourceMusic.loop = true;
            audioSourceMusic.Play();
        }

        public void Play2DSound(SoundPurpose purpose)
        {
            AudioClip clip = _sfxDictionary[purpose];
            audioSourceSFX.PlayOneShot(clip,  _volume);
        }
        
        private void OnAnyButtonClicked()
        {
            Play2DSound(SoundPurpose.UIButtonClick);
        }

        private void OnAnyPlatePressed()
        {
            Play2DSound(SoundPurpose.PlatePressed);
        }

        private void OnAnyTorchLit()
        {
            Play2DSound(SoundPurpose.TorchLight);
        }

        private void OnAnyJumpStarted()
        {
            Play2DSound(SoundPurpose.Jump);
        }

        private void OnAnyPlatformStateChanged()
        {
            Play2DSound(SoundPurpose.IceCrack);
        }

        private void OnAnyPlatformTotallyMelted()
        {
            Play2DSound(SoundPurpose.IceBreak);
        }

        private void OnDoorOpened()
        {
            Play2DSound(SoundPurpose.DoorOpen);
        }
    }
}