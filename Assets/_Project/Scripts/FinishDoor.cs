using UnityEngine;

namespace _Project.Scripts
{
    public class FinishDoor : MonoBehaviour
    {
        [SerializeField] private Level level;

        private void OnEnable()
        {
            level.OnAllTorchesActivated += OnAllTorchesActivated;
        }

        private void OnAllTorchesActivated()
        {
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            level.OnAllTorchesActivated -= OnAllTorchesActivated;
        }
    }
}