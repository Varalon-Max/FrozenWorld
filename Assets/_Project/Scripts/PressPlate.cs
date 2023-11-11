using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts
{
    public class PressPlate : ActivationMechanism
    {
        [SerializeField, Self] private Collider2D plateCollider;
        
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                InvokeOnPlatePressed();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                InvokeOnPlatePressed();
            }
        }
    }
}