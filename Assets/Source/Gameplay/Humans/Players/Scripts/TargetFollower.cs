using System;
using System.Collections;
using AYellowpaper;
using Gameplay.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Humans.Players
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private TargetFinder _targetFinder;

        private Coroutine _followingCoroutine;

        private void OnEnable()
        {
            _targetFinder.TargetSet += OnTargetSet;
            _targetFinder.TargetLost += OnTargetLost;
        }

        private void OnDisable()
        {
            _targetFinder.TargetSet -= OnTargetSet;
            _targetFinder.TargetLost -= OnTargetLost;
        }

        private void OnTargetSet(ITargetable target)
        {
            if (_followingCoroutine == null)
            {
                _followingCoroutine = StartCoroutine(Following(target.Position));
            }
        }

        private void OnTargetLost()
        {
            if (_followingCoroutine != null)
            {
                StopCoroutine(_followingCoroutine);
                _followingCoroutine = null;
            }
        }

        private IEnumerator Following(Vector3 location)
        {
            while (true)
            {
                yield return null;

                var myLocation = new Vector3(location.x, 0, location.z);
                
                
                transform.LookAt(myLocation);



            }
        }
    }
}