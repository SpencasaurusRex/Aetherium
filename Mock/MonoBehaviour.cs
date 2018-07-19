using System;
using System.Net;
using System.Security.Cryptography;

namespace UnityEngine
{
    public abstract class MonoBehaviour
    {
        protected Transform transform;

        public T GetComponent<T>() where T: MonoBehaviour
        {
            return null;
        }
    }
}
