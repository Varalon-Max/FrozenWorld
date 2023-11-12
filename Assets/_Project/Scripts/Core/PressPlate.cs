﻿using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class PressPlate : ActivationMechanism
    {
        [SerializeField, Self] private Collider2D plateCollider;
        [SerializeField] private GameObject pressedVisual;
        [SerializeField] private GameObject unpressedVisual;
        
        private readonly string _plateActionName = "PlatePressed";

        
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
            SoundManager.Instance.Play2DSound(_plateActionName);
            if (other.gameObject.GetComponent<Player.Player>())
            {
                SetVisualsPressed(true);
                InvokeOnPlatePressed();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Player.Player>())
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