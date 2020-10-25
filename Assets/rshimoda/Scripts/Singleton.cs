using UnityEngine;
 
/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;
 
    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }
 
            Debug.Log("Getting Singleton");
            lock (m_Lock)
            {
                Debug.Log("After Locking");
                if (m_Instance == null)
                {
                    Debug.Log("Creating for the first time");
                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));
 
                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
 
                        Debug.Log("Make instance persistent.");
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }
            return m_Instance;
        }
    }
 
 
    private void OnApplicationQuit()
    {
        Debug.Log("this singleton is going down");
        m_ShuttingDown = true;
    }
 
 
    private void OnDestroy()
    {
        Debug.Log("this singleton is going down - da revenge");
        m_ShuttingDown = true;
    }
}