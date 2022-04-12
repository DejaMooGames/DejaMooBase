using System.Collections;
using UnityEngine;

namespace DejaMoo.GameEvents
{
    public class DelayedGameEventListener : GameEventListener
    {
        [SerializeField] private float delay;
        private WaitForSeconds responseDelay;

        private void Awake()
        {
            responseDelay = new WaitForSeconds(delay);
        }

        public override void RaiseEvent()
        {
            StartCoroutine(DelayedResponse());
        }

        private IEnumerator DelayedResponse()
        {
            yield return responseDelay;
            response.Invoke();
        }
    }

    public class DelayedGameEventListener<T> : GameEventListener<T>
    {
        [SerializeField] private float delay;
        private WaitForSeconds responseDelay;

        private void Awake()
        {
            responseDelay = new WaitForSeconds(delay);
        }

        public override void RaiseEvent(T value)
        {
            StartCoroutine(DelayedResponse(value));
        }

        private IEnumerator DelayedResponse(T value)
        {
            yield return responseDelay;
            response.Invoke(value);
        }
    }
}
