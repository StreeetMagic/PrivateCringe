using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Interfaces;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private ITargetable _target;

    [SerializeField] private float FindRadius = 10f;
    [SerializeField] private float LoseRadius = 10.1f;

    public event Action<ITargetable> TargetSet;
    public event Action TargetLost;

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        if (_target == null)
        {
            if (TrySetTarget())
            {
                TargetSet?.Invoke(_target);
            }
        }
        else
        {
            if (TryLoseTarget())
            {
                TargetLost?.Invoke();
            }
        }
    }

    private bool TrySetTarget()
    {
        var colliders = Physics.OverlapSphere(transform.position, FindRadius);

        float minDistance = float.MaxValue;

        foreach (var collider1 in colliders)
        {
            if (collider1.TryGetComponent(out ITargetable target))
            {
                var distance = Vector3.SqrMagnitude(transform.position - collider1.gameObject.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    _target = target;
                    _target.SetTargetedOn();
                }
            }
        }

        if (_target != null)
        {
            return true;
        }

        return false;
    }

    private bool TryLoseTarget()
    {
        var distance = Vector3.SqrMagnitude(transform.position - _target.Position);

        if (distance > LoseRadius * LoseRadius)
        {
            _target.SetTargetedOff();
            _target = null;

            return true;
        }

        return false;
    }
}