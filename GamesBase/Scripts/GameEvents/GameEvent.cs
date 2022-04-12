using System;
using System.Collections.Generic;
using UnityEngine;

namespace DejaMoo.GameEvents
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "ScriptableObject/Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> listeners = new List<GameEventListener>();
        public event Action gameEvent;

        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public void Raise()
        {
            for (var i = listeners.Count - 1; i >= 0; --i)
            {
                listeners[i].RaiseEvent();
            }
            gameEvent?.Invoke();
        }
    }

    public class GameEvent<T> : ScriptableObject
    {
        private readonly List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();
        public event Action<T> gameEvent; 

        public void RegisterListener(GameEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnRegisterListener(GameEventListener<T> listener)
        {
            if (!listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public void Raise(T value)
        {
            for (var i = listeners.Count - 1; i >= 0; --i)
            {
                listeners[i].RaiseEvent(value);
            }
            gameEvent?.Invoke(value);
        }
    }
    
    public class GameEvent<T1, T2> : ScriptableObject
    {
        private readonly List<GameEventListener<T1, T2>> listeners = new List<GameEventListener<T1, T2>>();
        public void RegisterListener(GameEventListener<T1, T2> listener)
        {
            if(!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnRegisterListener(GameEventListener<T1, T2> listener)
        {
            if (!listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public void Raise(T1 value1, T2 value2)
        {
            for (var i = listeners.Count - 1; i >= 0; --i)
            {
                listeners[i].RaiseEvent(value1, value2);
            }
        }
    }
}


