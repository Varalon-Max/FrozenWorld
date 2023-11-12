using System;
using UnityEngine;

namespace _Project.Scripts
{
    public interface IPlayerController
    {
        public Vector2 Input { get; }
        public Vector2 Speed { get; }

        public event Action<bool> GroundedChanged; // Grounded - Impact force
        public event Action Jumped;
    }
}