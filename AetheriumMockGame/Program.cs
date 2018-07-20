using System;
using System.Threading;
using Aetherium;

namespace UnityEngine
{
    internal class Program
    {
        public static Scene TestScene()
        {
            var scene = new Scene();

            var ship = new GameObject("Ship");
            scene.AddGameObject(ship);
            ship.AddComponent<Ship>();

            return scene;
        }

        public static void Main()
        {
            GameEngine engine = new GameEngine();
            engine.LoadScene(TestScene());
            while (true)
            {
                Console.Clear();
                engine.Update();
                Thread.Sleep(100);
            }
        }
    }
}
