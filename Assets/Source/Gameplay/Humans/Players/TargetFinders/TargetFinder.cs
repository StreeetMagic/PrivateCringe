using System;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Humans.Players.TargetFinders
{
    public class TargetFinder : MonoBehaviour
    {
        private ITargetable _target;

        [SerializeField] private float FindRadius = 10f;
        [SerializeField] private float LoseRadius = 10.1f;

        public event Action<ITargetable> TargetSet;
        public event Action TargetLost;

        private void Update()
        {
            SearchForTargets();
        }

        private void SearchForTargets()
        {
            if (_target == null)
            {
                TrySetTarget();
            }
            else
            {
                TrySetTarget();
                TryLoseTarget();
            }
        }

        private void TrySetTarget()
        {
            var colliders = Physics.OverlapSphere(transform.position, FindRadius);
            float minDistance = float.MaxValue;
            
            var newTarget = TryGetTarget(colliders, minDistance);

            if (newTarget != null)
            {
                if (_target != newTarget)
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
                }
            }
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

        private void TryLoseTarget()
        {
            var distance = Vector3.SqrMagnitude(transform.position - _target.Position);

            if (distance > LoseRadius * LoseRadius)
            {
                _target.Missed -= OnLost;
                OnLost();
                TargetLost?.Invoke();
            }
        }

        private void OnLost()
        {
            _target.SetTargetedOff();
            _target = null;
            TargetLost?.Invoke();
        }
    }
}