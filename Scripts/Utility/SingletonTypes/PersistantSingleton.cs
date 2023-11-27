using UnityEngine;

namespace DP.Tools
{
    /// <summary>
    /// Persistant version of a singleton that will survive through scene loads. 
    /// </summary>
    public class PersistantSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}