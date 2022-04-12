using System;
using DejaMoo.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DejaMoo
{
    public class PlayerLoopProxy : Singleton<PlayerLoopProxy>
    {
        public static event Action MonoUpdate;
        public static event Action MonoFixedUpdate;
        public static event Action MonoAwake;
        public static event Action MonoStart;
        public static event Action MonoApplicationQuit;

        protected override void Awake()
        {
            base.Awake();
            OnMonoAwake();
            #if UNITY_EDITOR
            EditorApplication.playModeStateChanged += EditorApplication_OnPlayModeStateChanged;
            #endif
        }

        #if UNITY_EDITOR
        private static void EditorApplication_OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if(obj == PlayModeStateChange.ExitingPlayMode)
                OnMonoApplicationQuit();
        }
        #endif

        private void Start()
        {
            OnMonoStart();
        }

        private void Update()
        {
            OnMonoUpdate();
        }

        private void FixedUpdate()
        {
            OnMonoFixedUpdate();
        }

        private void OnApplicationQuit()
        {
            OnMonoApplicationQuit();
        }


        private static void OnMonoUpdate()
        {
            MonoUpdate?.Invoke();
        }

        private static void OnMonoFixedUpdate()
        {
            MonoFixedUpdate?.Invoke();
        }

        private static void OnMonoAwake()
        {
            MonoAwake?.Invoke();
        }

        private static void OnMonoStart()
        {
            MonoStart?.Invoke();
        }

        private static void OnMonoApplicationQuit()
        {
            MonoApplicationQuit?.Invoke();
        }
    }
}