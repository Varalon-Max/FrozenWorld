﻿using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class KillPlatform : MonoBehaviour
    {
        [SerializeField, Self] private Collider2D collider2D;
        
        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Player.Player>())
            {
                GameManager.Instance.HandlePlayerDead();
            }
        }
    }
}