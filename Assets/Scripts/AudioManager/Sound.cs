using UnityEngine;

namespace Game.Audio
{
    [System.Serializable]
    public struct Sound
    {
        [HideInInspector]
        public AudioSource audioSource;
        public string name;
        public AudioClip clip;

        [Range(0, 1)]
        public float volume;
        
        [Range(0,1)]
        public float spatialBlend;


        [Range(.1f, 3)]
        public float pitch;
        public bool shouldLoop;
    }
}
