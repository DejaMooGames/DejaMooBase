using UnityEngine;
using UnityEngine.Events;

namespace DejaMoo.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent response;

        protected virtual void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        protected virtual void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public virtual void RaiseEvent()
        {
            response.Invoke();
        }
    }

    public class GameEventListener<T> : MonoBehaviour
    {
        public GameEvent<T> gameEvent;
        public UnityEvent<T> response;
        
        protected  void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        protected  void OnDisable()
        {
            gameEvent.UnRegisterListener(this);
        }

        public virtual void RaiseEvent(T value)
        {
            response.Invoke(value);
        }
    }
    
    public class GameEventListener<T1, T2> : MonoBehaviour
    {
        public GameEvent<T1, T2> gameEvent;
        public UnityEvent<T1, T2> response;
        
        protected void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        protected void OnDisable()
        {
            gameEvent.UnRegisterListener(this);
        }

        public virtual void RaiseEvent(T1 value1, T2 value2)
        {
            response.Invoke(value1, value2);
        }
    }
}
