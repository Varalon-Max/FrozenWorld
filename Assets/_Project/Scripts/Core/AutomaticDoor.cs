using UnityEngine;

namespace _Project.Scripts.Core
{
    public class AutomaticDoor: MonoBehaviour
    {
        [SerializeField] private ActivationMechanism activationMechanism;

        private void OnEnable()
        {
            activationMechanism.OnPlatePressed += OpenDoor;
        }

        private void OpenDoor()
        {
            // TODO: add door opening animation
            Destroy(gameObject);
        }
        
        private void OnDisable()
        {
            activationMechanism.OnPlatePressed -= OpenDoor;
        }
    }
}