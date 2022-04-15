using System;
using System.Collections;
using System.Collections.Generic;
using DejaMoo.Extensions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace DejaMoo.RuntimeSets
{
    public class RuntimeSet<T> : ScriptableObject, IEnumerable<T>
    {
        private readonly List<T> set = new List<T>();
        [SerializeField] private List<T> editorList = new List<T>();
        private int position;
        public int Count => set.Count;
    
        public event Action<T> ElementAdded;
        public event Action<T> ElementRemoved;
        public event Action SetCleared;

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                PlayerLoopProxy.MonoAwake += PlayerLoopProxy_OnMonoAwake;
                PlayerLoopProxy.MonoApplicationQuit += PlayerLoopProxy_OnMonoApplicationQuit;
            }
#else
            PlayerLoopProxy.MonoAwake += PlayerLoopProxy_OnMonoAwake;
            PlayerLoopProxy.MonoApplicationQuit += PlayerLoopProxy_OnMonoApplicationQuit;
#endif
        }

        private void PlayerLoopProxy_OnMonoApplicationQuit()
        {
            PlayerLoopProxy.MonoAwake -= PlayerLoopProxy_OnMonoAwake;
            PlayerLoopProxy.MonoApplicationQuit -= PlayerLoopProxy_OnMonoApplicationQuit;
        }

        private void PlayerLoopProxy_OnMonoAwake()
        {
            Debug.Log("MonoAwake");
            Clear();
        }
        
        public virtual void Add(T element)
        {
            if (set.Contains(element)) return;
            set.Add(element);
            editorList.Add(element);
            OnElementAdded(element);
        }

        public virtual void Remove(T element)
        {
            if (!set.Contains(element)) return;
            set.Remove(element);
            editorList.Remove(element);
            OnElementRemoved(element);
        }

        public virtual T GetAndRemoveRandom()
        {
            var element = set.RandomItem();
            Remove(element);
            return element;
        }

        protected virtual void OnElementRemoved(T element)
        {
            ElementRemoved?.Invoke(element);
        }

        protected virtual void OnElementAdded(T element)
        {
            ElementAdded?.Invoke(element);
        }

        protected virtual void OnSetCleared()
        {
            SetCleared?.Invoke();
        }

        public void Clear()
        {
            set.Clear();
            editorList.Clear();
        }

        public List<T> GetCopy()
        {
            return new List<T>(set);
        }

        public T this[int index] => set[index];
        
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var t in set)
            {
                yield return t;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
