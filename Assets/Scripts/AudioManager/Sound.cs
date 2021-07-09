using UnityEngine;

namespace Game.Audio
{
    [System.Serializable]
    public struct Sound
    {
        [HideInInspector]
        public AudioSource source;
        public string name;
        public AudioClip clip;

        [Range(0, 1)]
        public float volume;

        [Range(.1f, 3)]
        public float pitch;
        public bool shouldLoop;
    }
}
