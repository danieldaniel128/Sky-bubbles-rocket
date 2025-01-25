using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private List<AudioSource> sfxSources= new List<AudioSource>();
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioClip Launch;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float bgmVolume = 1f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays a sound effect.
    /// </summary>
    /// <param name="clip">AudioClip to play.</param>
    public void PlaySFX(AudioClip clip,float vol )
    {
        if (clip == null) return;
        AudioSource source = null;
        foreach (var item in sfxSources)
        {
            if (!item.isPlaying)
            {
                source = item;
                break;
            }
        }
        source.volume = vol;
        source.PlayOneShot(clip);

    }

    /// <summary>
    /// Plays background music.
    /// </summary>
    /// <param name="clip">AudioClip to play.</param>
    /// <param name="loop">Whether the music should loop.</param>
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.volume = bgmVolume;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    /// <summary>
    /// Stops background music.
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }
    public void PlayRocket()
    {
        PlaySFX(Launch, 0.7f);
    }
    /// <summary>
    /// Adjusts the SFX volume.
    /// </summary>
    /// <param name="volume">New SFX volume (0.0 to 1.0).</param>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// Adjusts the BGM volume.
    /// </summary>
    /// <param name="volume">New BGM volume (0.0 to 1.0).</param>
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        bgmSource.volume = bgmVolume;
    }

    private void Update()
    {
        SetBGMVolume(bgmVolume);
    }
}
