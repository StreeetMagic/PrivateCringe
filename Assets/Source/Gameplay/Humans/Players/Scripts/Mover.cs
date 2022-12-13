using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Humans.Players
{
    public class Mover : MonoBehaviour, IChangePosition
    {
        private PlayerInputActions _playerInputActions;
        private InputAction _move;
        private Vector2 _moveDirection = Vector2.zero;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _move = _playerInputActions.Player.Move;
            _move.Enable();
        }

        private void OnDisable()
        {
            _move.Disable();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _moveDirection = _move.ReadValue<Vector2>();
            
            var direction = new Vector3(_moveDirection.x, 0, _moveDirection.y);
            
            transform.position += direction * 0.01f;
        }
    }
}