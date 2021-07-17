using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Audio;

namespace Game.GameUI
{
    public class InitSlider : MonoBehaviour
    {
        Slider slider = null;
        void Start()
        {
            slider = GetComponent<Slider>();
            // make sure the slider's value matches that of the corresponding scene music
            Sound s = AudioManager.instance.getSoundFromScene();
            slider.value = s.audioSource.volume;
        }

    }
}
