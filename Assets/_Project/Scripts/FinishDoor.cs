using UnityEngine;

namespace _Project.Scripts
{
    public class FinishDoor : MonoBehaviour
    {
        private Level _level;
        private readonly string _doorActionName = "OpenDoor";

        public static event System.Action OnDoorOpened;
        private void Awake()
        {
            _level = FindObjectOfType<Level>();
        }

        private void OnEnable()
        {
            _level.OnAllTorchesActivated += OnAllTorchesActivated;
        }

        private void OnAllTorchesActivated()
        {
            OnDoorOpened?.Invoke();
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            _level.OnAllTorchesActivated -= OnAllTorchesActivated;
        }
    }
}