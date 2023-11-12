using System;
using _Project.Scripts.Tools;
using Eflatun.SceneReference;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts
{
    public class NextLevelTransition : MonoBehaviour
    {
        [SerializeField] private SceneReference nextScene;
        [SerializeField, Self] private Collider2D transitionCollider2D;

        private void OnValidate()
        {
            this.ValidateRefs();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Player.Player>())
            {
                Loader.Load(nextScene);
            }
        }
    }
}