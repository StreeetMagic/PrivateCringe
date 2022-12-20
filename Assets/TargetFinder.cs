using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Interfaces;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.TryGetComponent(out ITargetable targetable))
        {
            print(other.gameObject.name);
        }
    }
}