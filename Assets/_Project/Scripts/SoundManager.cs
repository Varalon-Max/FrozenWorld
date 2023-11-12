using UnityEngine;

namespace _Project.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        private float _volume = 1f;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }   
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        public void Play2DSound(AudioClip clip)
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, _volume);
        }
    }
}