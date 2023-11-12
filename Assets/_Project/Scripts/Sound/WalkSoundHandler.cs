using System;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts
{
    public class WalkSoundHandler: MonoBehaviour
    {
        [SerializeField] private AudioClip walkAudioClip;
        [SerializeField] private AudioSource walkSource;
        [SerializeField] private float pauseBetweenSteps;

        private float _timeCounter;
        private float _volume = 1f;

        private void Start()
        {
            _timeCounter = 0;
        }
        

        private void Update()
        {
            _timeCounter -= Time.deltaTime; 
            if (_timeCounter<=0 && MoveController.Input.x!=0 && MoveController.OnGround)
            {
                walkSource.PlayOneShot(walkAudioClip,_volume);
                _timeCounter = pauseBetweenSteps;
            }
        }
    }
}