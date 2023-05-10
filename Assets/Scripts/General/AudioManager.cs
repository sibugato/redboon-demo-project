using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum MusicType
    {
        none,
        market
    }

    public enum SoundType
    {
        coin_chink
    }

    public static AudioManager Audio;

    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _soundAudioSource;

    [SerializeField] private List<AudioClip> _musicList;
    [SerializeField] private List<AudioClip> _soundList;

    /*
    MUSIC
    0 - market

    SOUND
    0 -  coin chink
    */

    private void Awake()
    {
        // Singleton
        if (Audio == null)
        {
            Audio = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMusic(MusicType type)
    {
        switch (type)
        {
            case MusicType.none:
                {
                    _musicAudioSource.clip = null;
                    break;
                }
            case MusicType.market:
                {
                    _musicAudioSource.clip = _musicList[0];
                    break;
                }
        }
        _musicAudioSource.Play();
    }

    public void SoundPlayOneShot(SoundType type)
    {
        switch (type)
        {
            case SoundType.coin_chink:
                {
                    _soundAudioSource.PlayOneShot(_soundList[0]);
                    break;
                }
        }
    }
}
