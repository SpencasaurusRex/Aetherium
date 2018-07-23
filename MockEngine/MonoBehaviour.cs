using System;

namespace UnityEngine
{
    public enum ComponentState
    {
        Inactive,
        Awake,
        Active,
        MarkedForDeletion
    }

    public abstract class MonoBehaviour
    {
        protected Transform transform;
        public ComponentState State { get; set; }

        public T GetComponent<T>() where T: MonoBehaviour
        {
            return null;
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {
        }
    }
}
