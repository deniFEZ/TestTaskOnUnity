using System.Collections.Generic;
using System;
using UnityEngine;

public class Eventbus : MonoBehaviour
{
    protected Dictionary<Type, Delegate> m_Observers = new();
    public static Eventbus Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public virtual void Subscribe<U>(Action<U> callback)
    {
        var eventType = typeof(U);
        if (m_Observers.ContainsKey(eventType))
        {
            m_Observers[eventType] = Delegate.Combine(m_Observers[eventType], callback);
        }
        else
        {
            m_Observers[eventType] = callback;
        }
    }

    public virtual void Unsubscribe<U>(Action<U> callback)
    {
        var eventType = typeof(U);
        if (!m_Observers.ContainsKey(eventType)) return;

        var currentDelegate = m_Observers[eventType];
        var newDelegate = Delegate.Remove(currentDelegate, callback);

        if (newDelegate == null) m_Observers.Remove(eventType);
        else m_Observers[eventType] = newDelegate;
    }

    public virtual void Publish<U>(U eventData)
    {
        var eventType = typeof(U);
        if (m_Observers.TryGetValue(eventType, out var del))
        {
            if (del is Action<U> callback)
            {
                callback.Invoke(eventData);
            }
        }
    }
}