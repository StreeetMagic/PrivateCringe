using System;
using Gameplay.Interfaces;
using UnityEngine;
using AYellowpaper;
using TMPro;

namespace UI
{
    public class HealthDisplayer : MonoBehaviour
    {
        public InterfaceReference<IDamageable> Target;

        private IDamageable _target => Target.Value;

        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _target.HealthChanged += Display;
        }
        private void OnDisable()
        {
            _target.HealthChanged -= Display;
        }

        private void Display(int health, int maxHealth)
        {
            var text = health + " / " +  maxHealth + " HP";
            _text.text = text;
        }

    }
}