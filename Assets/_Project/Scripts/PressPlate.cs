using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts
{
    public class PressPlate : ActivationMechanism
    {
        [SerializeField, Self] private Collider2D plateCollider;
        [SerializeField] private GameObject pressedVisual;
        [SerializeField] private GameObject unpressedVisual;
        
        private void OnValidate()
        {
            this.ValidateRefs();
        }
        
        private void Awake()
        {
            pressedVisual.SetActive(false);
            unpressedVisual.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                SetVisualsPressed(true);
                InvokeOnPlatePressed();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                SetVisualsPressed(false);
                InvokeOnPlatePressed();
            }
        }
        
        private void SetVisualsPressed(bool pressed)
        {
            pressedVisual.SetActive(pressed);
            unpressedVisual.SetActive(!pressed);
        }
    }
}