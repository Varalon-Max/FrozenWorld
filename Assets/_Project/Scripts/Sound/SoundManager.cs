using System;
using System.Security.Cryptography;
using UnityEngine;

namespace _Project.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        [SerializeField] private Sound[] musicSound;
        [SerializeField] private Sound[] sfxSound;

        private float _volume = 1f;
        private void Awake()
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
        
        public void Play2DSound(string name)
        {
            Sound sound = Array.Find(sfxSound, x => x.Name == name);
            AudioSource.PlayClipAtPoint(sound.AudioClip, Vector3.zero, _volume);
        }
    }
}