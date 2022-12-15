using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class NormalToCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            RotateToCamera();
        }

        private void RotateToCamera()
        {
            transform.LookAt(transform.position + _camera.transform.forward);
        }
    }
}