using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioSource propulsorSource;
    public AudioSource ambienteSource;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    private void Awake()
    {
        if (instance == null) instance = this; else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayPropulsor(AudioClip clip)
    {
        propulsorSource.clip = clip;
        propulsorSource.Play();
    }

    public void PlayAmbiente(AudioClip clip)
    {
        ambienteSource.clip = clip;
        ambienteSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
