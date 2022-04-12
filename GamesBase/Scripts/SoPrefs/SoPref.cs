using DejaMoo.GameEvents;
using DejaMoo.VariableReferences;
using UnityEditor;
using UnityEngine;

namespace DejaMoo.SoPrefs
{
    public abstract class SoPref<T> : ScriptableObject, IResetablePref
    {
        [SerializeField] protected StringReference key;
        [SerializeField] public GameEvent<T> valueChanged;
        [SerializeField] protected T defaultValue;
        [SerializeField, ShowOnly] protected T editorValue;

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                Debug.Log("Scriptable Object OnEnable");
                PlayerLoopProxy.MonoAwake += PlayerLoopProxy_OnMonoAwake;
                PlayerLoopProxy.MonoApplicationQuit += PlayerLoopProxy_OnMonoApplicationQuit;
            }
#else
            Debug.Log("Scriptable Object OnEnable");
            PlayerLoopProxy.MonoAwake += PlayerLoopProxy_OnMonoAwake;
            PlayerLoopProxy.MonoApplicationQuit += PlayerLoopProxy_OnMonoApplicationQuit;
#endif
        }

        private void PlayerLoopProxy_OnMonoApplicationQuit()
        {
            
        }
        
        private void PlayerLoopProxy_OnMonoAwake()
        {
            Debug.Log("MonoAwake");
            Preferences.Instance.RegisterPref(this);
        }
        
        public abstract T Value
        {
            get;
            set;
        }

        [ContextMenu("Set pref to defaultValue")]
        public void ResetPref()
        {
            Value = defaultValue;
        }
    }
}