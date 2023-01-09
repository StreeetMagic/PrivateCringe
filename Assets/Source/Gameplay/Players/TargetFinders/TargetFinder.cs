using System;
using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Players.TargetFinders
{
    public class TargetFinder : MonoBehaviour
    {
        private const float FindRadius = 20f;
        private const float LoseRadius = 20.5f;
        private const float Cooldown = .1f;

        private ITargetable _target;
        private Coroutine _searchingTargets;

        public event Action<ITargetable> TargetSet;
        public event Action TargetLost;

        private void Start()
        {
            if (_searchingTargets == null)
            {
                _searchingTargets = StartCoroutine(SearchingForTargets());
            }
        }

        private IEnumerator SearchingForTargets()
        {
            var pause = new WaitForSeconds(Cooldown);

            while (true)
            {
                if (TrySetTarget() == false)
                {
                    TryLoseTarget();
                }

                yield return pause;
            }
        }

        private bool TrySetTarget()
        {
            var colliders = Physics.OverlapSphere(transform.position, FindRadius);
            float minDistance = float.MaxValue;

            var newTarget = TryGetTarget(colliders, minDistance);

            if (newTarget != null)
            {
                if (_target == newTarget)
                {
                    TargetSet?.Invoke(_target);
                }
                else
                {
                    if (_target != null)
                    {
                        _target.SetTargetedOff();
                        _target.Missed -= OnLost;
                        TargetLost?.Invoke();
                    }

                    _target = newTarget;
                    _target.Missed += OnLost;
                    _target.SetTargetedOn();
                    TargetSet?.Invoke(_target);

                    return true;
                }
            }

            return false;
        }

        private ITargetable TryGetTarget(Collider[] colliders, float minDistance)
        {
            ITargetable newTarget = null;

            foreach (var collider1 in colliders)
            {
                if (collider1.TryGetComponent(out ITargetable target))
                {
                    var distance = Vector3.SqrMagnitude(transform.position - collider1.gameObject.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newTarget = target;
                    }
                }
            }

            return newTarget;
        }

        private bool TryLoseTarget()
        {
            if (_target == null)
            {
                return false;
            }

            var distance = Vector3.SqrMagnitude(transform.position - _target.Position);

            if (distance > LoseRadius * LoseRadius)
            {
                _target.Missed -= OnLost;
                OnLost();
                TargetLost?.Invoke();

                return true;
            }

            return false;
        }

        private void OnLost()
        {
            if (_target != null)
            {
                _target.SetTargetedOff();
                _target = null;
            }

            TargetLost?.Invoke();
        }
    }
}