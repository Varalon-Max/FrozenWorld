using System;

namespace _Project.Scripts.Behaviours
{
    public interface IMeltingBehaviour
    {
        public static event Action OnAnyStateChanged;
        void Awake();
        void OnEnable();
        void OnDisable();
        
        protected static void InvokeOnAnyStateChanged()
        {
            OnAnyStateChanged?.Invoke();
        }
    }
}