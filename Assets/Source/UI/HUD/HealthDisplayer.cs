using AYellowpaper;
using Gameplay.Interfaces;
using TMPro;
using UnityEngine;

namespace UI.HUD
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