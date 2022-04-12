using UnityEngine;

namespace DejaMoo.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                //if the instance is null spawn a GameObject with this component attached
                if (instance is null)
                {
                    // ReSharper disable once ObjectCreationAsStatement
                    new GameObject(typeof(T).Name, typeof(T));
                }
                return instance;
            }
            private set
            {
                if (instance != null)
                {
                    Destroy(instance.gameObject);
                }
                instance = value;
            }
        }
        
        protected virtual void Awake()
        {
            Instance = (T)this;
        }
    }
}