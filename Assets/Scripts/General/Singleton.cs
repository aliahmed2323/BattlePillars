using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public bool ShouldPersistOnLoad = false;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>(true);
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }
            }

            if (instance is Singleton<T> singleton)
            {
                if (singleton.ShouldPersistOnLoad)
                {
                    DontDestroyOnLoad(singleton.gameObject);
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (ShouldPersistOnLoad)
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
