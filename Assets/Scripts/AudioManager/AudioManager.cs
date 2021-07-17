using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public static AudioManager instance;
        private bool isPlayingMusic = false;
        private enum MusicState
        {
            MenuHipHop,
            GameTheme,
            SickBeat,
        }
        private MusicState musicState = MusicState.MenuHipHop;
        private Dictionary<int, string> sceneIndexToMusic = new Dictionary<int, string>();

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
                sounds[i].audioSource = gameObject.AddComponent<AudioSource>();
                sounds[i].audioSource.playOnAwake = false;
                sounds[i].audioSource.clip = sounds[i].clip;

                sounds[i].audioSource.volume = sounds[i].volume;
                sounds[i].audioSource.spatialBlend = sounds[i].spatialBlend;
                sounds[i].audioSource.pitch = sounds[i].pitch;
                sounds[i].audioSource.loop = sounds[i].shouldLoop;
            }

            // build dict
            string[] soundNames = {"SickBeat","GameTheme","SickBeat"};
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
            {
                sceneIndexToMusic.Add(i, soundNames[i]);
            }
        }

        void Start()
        {
            // Play the game theme here
            Play("SickBeat", volume: 1);
        }

        void Update()
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                // menu and credits scene
                case 0:
                case 2:
                    if (musicState != MusicState.SickBeat)
                    {
                        StopPlayingAllMusic();
                        UnityEngine.Debug.Log($"playing sick beat");
                        Play("SickBeat", volume:1);
                        musicState = MusicState.SickBeat;
                    }
                    break;
                // level 1 scene
                case 1:
                    if (musicState != MusicState.GameTheme)
                    {
                        StopPlayingAllMusic();
                        Play("GameTheme", volume: 1);
                        musicState = MusicState.GameTheme;
                    }
                    break;
            }
        }

        public Sound getSoundFromScene()
        {
            string soundName = sceneIndexToMusic[SceneManager.GetActiveScene().buildIndex];
            // UnityEngine.Debug.Log($"soundName: {soundName}");

            // find the correct sound struct according to the sound name
            // should always find it in this case
            Sound s = Array.Find<Sound>(sounds, sound => sound.name == soundName);
            if (s.Equals(default(Sound)))
            {
                Debug.LogWarning($"Sound: \"{s.name}\" not found for getSoundFromScene().");
                return default(Sound);
            }

            return s;
        }

        public void setVolume(float volume)
        {
            // toggle sound
            getSoundFromScene().audioSource.volume = volume;
        }

        public void Play(string name, float volume = .5f, bool playOneShot = false)
        {
            Sound s = Array.Find<Sound>(sounds, sound => sound.name == name);

            if (s.Equals(default(Sound)))
            {
                Debug.LogWarning($"Sound: \"{s.name}\" not found for Play()");
                return;
            }

            if (playOneShot)
            {
                // using playoneshot instead of play() to play thru the entire sound
                s.audioSource.PlayOneShot(s.audioSource.clip, volume);
            }
            else
            {
                s.audioSource.Play();
            }
        }

        private void Stop(string name)
        {
            Sound s = Array.Find<Sound>(sounds, sound => sound.name == name);

            if (s.Equals(default(Sound)))
            {
                Debug.LogWarning($"Sound: \"{s.name}\" not found");
                return;
            }

            s.audioSource.Stop();
        }

        private void StopPlayingAllMusic()
        {
            foreach (Sound s in sounds)
            {
                if (s.audioSource.isPlaying)
                {
                    s.audioSource.Stop();
                }
            }
        }
    }
}
