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
            _offset = transform.position - _player.transform.position;
        }

        private void LateUpdate()
        {

            transform.position = _player.transform.position + _offset;
        }
    }
}
