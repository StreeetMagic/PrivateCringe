using UnityEngine;

namespace UI.HUD
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