using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts
{
    public class HeatingArea : MonoBehaviour
    {
        [SerializeField, Self] private Collider2D heatingAreaCollider;

        private void OnValidate()
        {
            this.ValidateRefs();
        }
    }
}