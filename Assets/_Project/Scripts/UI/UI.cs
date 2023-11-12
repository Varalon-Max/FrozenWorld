using System;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class UI : MonoBehaviour
    {
        public static event Action OnAnyButtonClicked;
        
        protected void InvokeOnAnyButtonClicked()
        {
            OnAnyButtonClicked?.Invoke();
        }
    }
}