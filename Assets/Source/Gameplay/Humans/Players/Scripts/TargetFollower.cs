using System.Collections;
using AYellowpaper;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Humans.Players
{
    public class TargetFollower : MonoBehaviour
    {
        public ITargetable Target;

        public Coroutine PauseCoroutine;

        private void Update()
        {
            if (Target == null)
            {
                if (TrySetTarget(out ITargetable target))
                {
                    Target = target;
                }
            }
            else if (Target != null)
            {
                Follow();
            }
        }

        private void Follow()
        {
            transform.LookAt(Target.Position);
        }

        private bool TrySetTarget(out ITargetable target)
        {
            if (PauseCoroutine == null)
            {
                Debug.Log("Запускаю карутину");
                PauseCoroutine = StartCoroutine(Pause());
            }

            target = null;

            return false;
        }

        private IEnumerator Pause()
        {
            Debug.Log("Начал поиск цели");

            yield return new WaitForSeconds(.2f);
            StopCoroutine(PauseCoroutine);
            PauseCoroutine = null;
            Debug.Log("Останавливаю корутину");
        }
    }
}