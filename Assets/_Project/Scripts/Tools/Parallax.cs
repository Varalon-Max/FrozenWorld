using System;
using UnityEngine;

namespace _Project.Scripts.Tools
{
    public class Parallax: MonoBehaviour
    {
        [Serializable]
        public struct ParallaxLayer
        {
            public Transform transform;
            public float speed;
        }

        [SerializeField] private ParallaxLayer[] parallaxLayers;

        private Vector3 _lastCameraPosition;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _lastCameraPosition = _mainCamera.transform.position;
        }

        private void Update()
        {
            Vector3 deltaMovement = _mainCamera.transform.position - _lastCameraPosition;

            foreach (var layer in parallaxLayers)
            {
                float parallaxFactor = layer.speed;
                Vector3 parallaxEffect = deltaMovement * parallaxFactor;

                Vector3 layerTargetPosition = layer.transform.position + parallaxEffect;
                layer.transform.position = Vector3.Lerp(layer.transform.position, layerTargetPosition, Time.deltaTime);
            }

            _lastCameraPosition = _mainCamera.transform.position;
        }
    }
}