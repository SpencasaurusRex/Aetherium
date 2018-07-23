using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine
{
    public class Scene
    {
        readonly List<GameObject> gameObjects;
        readonly List<GameObject> gameObjectsToAdd;
        readonly List<GameObject> gameObjectsToDestroy;

        public Scene()
        {
            gameObjects = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToDestroy = new List<GameObject>();
        }

        public void AddGameObject(GameObject go)
        {
            if (gameObjects.Contains(go)) return;
            gameObjectsToAdd.Add(go);
            go.ContainerScene = this;
            // TODO Distinct name checking
        }

        public void DestroyObject(GameObject go)
        {
            if (!gameObjects.Contains(go)) return;
            gameObjectsToDestroy.Add(go);
        }

        public void Update()
        {
            foreach (var go in gameObjectsToDestroy)
            {
                gameObjects.Remove(go);
            }

            foreach (var go in gameObjectsToAdd)
            {
                gameObjects.Add(go);
            }

            foreach (var go in gameObjects)
            {
                go.Update();
            }
        }

        public void FixedUpdate()
        {
            foreach (var go in gameObjects)
            {
                go.FixedUpdate();
            }
        }
    }
}