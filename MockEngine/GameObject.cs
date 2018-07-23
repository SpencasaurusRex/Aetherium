using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class GameObject
    {
        public Scene ContainerScene { get; set; }

        public string Name { get; private set; }

        public GameObject(string name)
        {
            Name = name;
        }

        public Transform transform { get; private set; }
        List<MonoBehaviour> components;
        HashSet<Type> types;

        public T AddComponent<T>() where T : MonoBehaviour, new()
        {
            if (types.Contains(typeof(T))) return null;
            var comp = new T();
            comp.State = ComponentState.Inactive;
            components.Add(comp);
            return comp;
        }

        public void Destroy()
        {
            foreach (var component in components)
            {
                component.State = ComponentState.MarkedForDeletion;
            }

            ContainerScene.DestroyObject(this);
        }

        public void Update()
        {
            foreach (var component in components)
            {
                switch (component.State)
                {
                    case ComponentState.Inactive:
                        component.Awake();
                        break;
                    case ComponentState.Awake:
                        component.Start();
                        break;
                    case ComponentState.Active:
                        component.Update();
                        break;
                }
            }
        }

        public void FixedUpdate()
        {
            foreach (var component in components)
            {
                if (component.State != ComponentState.Active) return;
                component.FixedUpdate();
            }
        }
    }
}