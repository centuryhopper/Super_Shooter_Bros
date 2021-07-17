using UnityEngine;


namespace Game.singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // create a new game object during runtime
                    GameObject g = new GameObject();

                    // add component to it
                    instance = g.AddComponent<T>();

                    // give it a name
                    g.name = $"Runtime-Spawned: {typeof(T).ToString()}";
                }

                return instance;
            }

        }

    }
}