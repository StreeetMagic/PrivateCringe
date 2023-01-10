using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.ReloaderBars
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] protected Image ProgressBarImage;
        

        private void Start()
        {
            ProgressBarImage.fillAmount = 0;
            ProgressBarImage.gameObject.SetActive(false);
        }

        protected void Draw(float duration)
        {
            StartCoroutine(Drawing(duration));
        }

        private IEnumerator Drawing(float duration)
        {
            ProgressBarImage.gameObject.SetActive(true);
            float originalAmount = ProgressBarImage.fillAmount;

            for (float t = 0f; t < 1f; t += Time.deltaTime / duration)
            {
                var number = Mathf.Lerp(originalAmount, 1, t);
                ProgressBarImage.fillAmount = number;

                yield return null;
            }

            ProgressBarImage.fillAmount = 0;
            ProgressBarImage.gameObject.SetActive(false);
        }
    }
}