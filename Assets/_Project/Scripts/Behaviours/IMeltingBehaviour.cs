using System;

namespace _Project.Scripts.Behaviours
{
    public interface IMeltingBehaviour
    {
        public event Action OnAnyStateChanged;
        void Awake();
        void OnEnable();
        void OnDisable();
    }
}