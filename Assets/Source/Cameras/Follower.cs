using System;
using AYellowpaper;
using Gameplay.Interfaces;
using UnityEngine;

namespace Cameras
{
    public class Follower : MonoBehaviour
    {
        public InterfaceReference<IChangePosition> Player;

        private IChangePosition _player => Player.Value;
        
        
        private Vector3 _offset;

        private void Start()
        {
            SetOffset();
        }

        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            transform.position = _player.Position + _offset;
        }

        private void SetOffset()
        {
            _offset = transform.position - _player.Position;
        }
    }
}