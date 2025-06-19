using UnityEngine;

namespace Utils
{
    [DisallowMultipleComponent]
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
#pragma warning disable CS0649

        private static T instance;

        #region Properties
        protected virtual bool Persistent { get; }
        #endregion

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                if (Persistent)
                    DontDestroyOnLoad(gameObject);
            }
            else if (instance.gameObject != gameObject)
            {
                DestroyAllNonInstances();
            }
        }

        public static T Instance
        {
            get
            {
                if (instance) 
                    return instance;

                GameObject g = new GameObject($"{typeof(T).Name} [Generated]");
                instance = g.AddComponent<T>();
                DontDestroyOnLoad(g);

                return instance;
            }
        }
    
        private static void DestroyAllNonInstances()
        {
            foreach (var go in FindObjectsByType<T>(FindObjectsSortMode.None))
            {
                if (go != instance)
                {
                    Destroy(go);
                }
            }
        }
    }
}
