using System;
using Gameplay.Humans.Players;
using UnityEngine;

namespace Cameras
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private Player _player;
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
            transform.position = _player.transform.position + _offset;
        }

        private void SetOffset()
        {
            _offset = transform.position - _player.transform.position;
        }
    }
}