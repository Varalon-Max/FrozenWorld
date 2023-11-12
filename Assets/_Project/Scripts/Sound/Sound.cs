using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class Sound
    {
        public SoundPurpose Purpose;
        public AudioClip AudioClip;
    }

    public enum SoundPurpose
    {
        DoorOpen,
        IceBreak,
        IceCrack,
        Jump,
        TorchLight,
        PlatePressed,
        Run,
        UIButtonClick,
    }
}