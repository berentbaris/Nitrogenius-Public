using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public SoundType type;
    public AudioClip[] clips;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    public bool Loop;
    public float PitchMin = 1;
    public float PitchMax = 1;
    [HideInInspector] public AudioSource source;

    public void PlaySound(float delay = 0)
    {
        if (clips.Length > 1)
        {
            source.clip = clips[Random.Range(0, clips.Length)];
        }
        else
        {
            source.clip = clips[0];
        }
        source.pitch = Random.Range(PitchMin, PitchMax);
        source.volume = volume;
        source.loop = Loop;


        if (delay > 0)
        {
            source.PlayDelayed(delay);
        }

        else if (source.loop == true)
        {
            source.Play();
        }

        else
        {
            source.PlayOneShot(source.clip);
        }
    }

    public void StopPlaying(float fadetime = 0f, float delay = 0)
    {
        source.DOFade(0, fadetime).SetDelay(delay).OnComplete(() => StopAudioSource());
    }

    private void StopAudioSource()
    {
        source.Stop();
    }
}

public enum SoundType
{
    SFX,
    Music
}