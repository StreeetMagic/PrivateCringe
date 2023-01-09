using System.Collections;
using Gameplay.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.ReloaderBars
{
    public class ReloaderBar : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private Weapon _weapon;

        private void OnEnable()
        {
            _weapon.ReloadStarted += Draw;
        }

        private void OnDisable()
        {
            _weapon.ReloadStarted -= Draw;
        }

        private void Start()
        {
            _progressBar.fillAmount = 0;
            _progressBar.gameObject.SetActive(false);
        }

        private void Draw(float duration)
        {
            StartCoroutine(Drawing(duration));
        }

        private IEnumerator Drawing(float duration)
        {
            _progressBar.gameObject.SetActive(true);
            float originalAmount = _progressBar.fillAmount;

            for (float t = 0f; t < 1f; t += Time.deltaTime / duration)
            {
                var number = Mathf.Lerp(originalAmount, 1, t);
                _progressBar.fillAmount = number;

                yield return null;
            }

            _progressBar.fillAmount = 0;
            _progressBar.gameObject.SetActive(false);
        }
    }
}