using UnityEngine;
using System;

namespace Game.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        public static AudioManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // persist the audio manager between scenes
            DontDestroyOnLoad(gameObject);

            // add an audio source for each sound
            for (int i = 0; i < sounds.Length; i++)
            {
                // populate audiosource
                sounds[i].source = gameObject.AddComponent<AudioSource>();
                sounds[i].source.playOnAwake = false;
                sounds[i].source.clip = sounds[i].clip;

                sounds[i].source.volume = sounds[i].volume;
                sounds[i].source.pitch = sounds[i].pitch;
                sounds[i].source.loop = sounds[i].shouldLoop;
            }
        }

        // TODO add a main music theme sometime
        void Start()
        {
            // Play the game theme here
            Play("GameTheme", volume: 1);
        }

        public void Play(string name, float volume = .5f, bool playOneShot = false)
        {
            Sound s = Array.Find<Sound>(sounds, sound => sound.name == name);

            if (s.Equals(default(Sound)))
            {
                Debug.LogWarning($"Sound: \"{s.name}\" not found");
            }

            if (playOneShot)
            {
                // using playoneshot instead of play() to play thru the entire sound
                s.source.PlayOneShot(s.source.clip, volume);
            }
            else
            {
                s.source.Play();
            }


        }
    }
}
