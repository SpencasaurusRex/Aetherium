using System.Collections.Generic;

namespace UnityEngine
{
    public class GameEngine
    {
        List<Scene> activeScenes;

        public void LoadScene(Scene scene)
        {
            activeScenes.Add(scene);
        }

        public void UnloadScene(Scene scene)
        {
            activeScenes.Remove(scene);
        }

        public void Update()
        {
            foreach (var scene in activeScenes)
            {
                scene.Update();
            }

            foreach (var scene in activeScenes)
            {
                scene.FixedUpdate();
            }
        }
    }
}