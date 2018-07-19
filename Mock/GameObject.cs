using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class GameObject
    {
        public Transform transform { get; private set; }
        List<MonoBehaviour> components;
        HashSet<Type> types;

        public T AddComponent<T>() where T : MonoBehaviour, new()
        {
            if (types.Contains(typeof(T))) return null;
            return new T();
        }
    }
}
