using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Slider volumeSlider;

    void Start () {
        volumeSlider.value = SoundManager.instance.efxSource.volume;
    }

	void Update () {
        VolumeController();
    }

    public void VolumeController()
    {
        SoundManager.instance.efxSource.volume = volumeSlider.value;
        SoundManager.instance.musicSource.volume = volumeSlider.value;
        SoundManager.instance.propulsorSource.volume = volumeSlider.value;
        SoundManager.instance.ambienteSource.volume = volumeSlider.value;
        
    }
}
