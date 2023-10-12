using System;
using UnityEngine;

namespace JustGame.Script.Event
{
    /// <summary>
    /// Base class for all scriptable events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ScriptableEvent<T> : ScriptableObject
    {
        protected Action<T> m_listeners;

        public void AddListener(Action<T> toAddListener)
        {
            m_listeners += toAddListener;
        }

        public void RemoveListener(Action<T> toRemoveListener)
        {
            m_listeners -= toRemoveListener;
        }

        public void RaiseEvent(T value)
        {
            m_listeners?.Invoke(value);
        }
    }
}
