using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private static float _mainVolume = 1;
    private static float _musicVolume = 0.5f;
    private static float _sfxVolume = 1;

    private static List<AudioSource> _playingMusics;
    private static List<AudioSource> _playingSFXs;    
    private static Dictionary<string, GameObject> _allMusics;
    private static Dictionary<string, GameObject> _allSFXs;    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            _playingMusics = new List<AudioSource>();
            _playingSFXs = new List<AudioSource>();
            _allMusics = new Dictionary<string, GameObject>();
            _allSFXs = new Dictionary<string, GameObject>();
            PreLoadMusic();
            PreLoadSFX();
        }
    }

    private void Start()
    {
        PlayMusic("Main_Theme");
    }

    private void Update()
    {
        _playingMusics = RemoveNotPlayingSound(_playingMusics);
        _playingSFXs = RemoveNotPlayingSound(_playingSFXs);
    }

    private List<AudioSource> RemoveNotPlayingSound(List<AudioSource> listSounds)
    {
        List<AudioSource> listToReturn = listSounds.Where(source => source.isPlaying).ToList();
        return listToReturn;
    }

    private void PreLoadMusic()
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Musics/");
        foreach (AudioClip audioClip in audioClips)
        {
            GameObject currentAudio = new GameObject(audioClip.name);
            AudioSource currentAudioSource = currentAudio.AddComponent<AudioSource>();
            currentAudioSource.clip = audioClip;
            currentAudioSource.playOnAwake = false;
            currentAudio.transform.parent = transform;
            _allMusics.Add(currentAudio.name, currentAudio);
        }
    }
    
    private void PreLoadSFX()
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("SFX/");
        foreach (AudioClip audioClip in audioClips)
        {
            GameObject currentAudio = new GameObject(audioClip.name);
            AudioSource currentAudioSource = currentAudio.AddComponent<AudioSource>();
            currentAudioSource.clip = audioClip;
            currentAudioSource.playOnAwake = false;
            currentAudio.transform.parent = transform;
            _allSFXs.Add(currentAudio.name, currentAudio);
        }
    }
    
    public static void UpdateMainVolume(float mainVolume)
    {
        _mainVolume = mainVolume;
        UpdateVolume(_playingMusics, _mainVolume * _musicVolume);
        UpdateVolume(_playingSFXs, _mainVolume * _sfxVolume);
    }
    
    public static void UpdateMusicVolume(float musicVolume)
    {
        _musicVolume = musicVolume;
        UpdateVolume(_playingMusics, _mainVolume * _musicVolume);
    }
    
    public static void UpdateSFXVolume(float sfxVolume)
    {
        _sfxVolume = sfxVolume;
        UpdateVolume(_playingSFXs, _mainVolume * _sfxVolume);
    }

    private static void UpdateVolume(List<AudioSource> audioSources, float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    public static void PlayMusic(string musicName)
    {
        _playingMusics = PlaySound(musicName, _allMusics, _playingMusics, _mainVolume*_musicVolume, true);
    }
    
    public static void PlaySFX(string sfxName)
    {
        _playingSFXs = PlaySound(sfxName, _allSFXs, _playingSFXs, _mainVolume*_sfxVolume, false);
    }

    public static void StopMusic(string musicName)
    {
        _playingMusics= StopSound(musicName, _allMusics, _playingMusics);
    }

    public static void StopSFX(string sfxName)
    {
        _playingSFXs= StopSound(sfxName, _allSFXs, _playingSFXs);
    }

    private static List<AudioSource> PlaySound(string soundName, Dictionary<string, GameObject> dictionary, List<AudioSource> playingSoundList, float volume, bool repeat)
    {
            if (dictionary.TryGetValue(soundName, out GameObject objectToPlay))
            {
                AudioSource soundToPlay = objectToPlay.GetComponent<AudioSource>();
                if (playingSoundList.Contains(soundToPlay))
                {
                    Debug.LogError(soundName + " is already playing !");
                }
                else
                {
                    soundToPlay.loop = repeat;
                    soundToPlay.volume = volume;
                    soundToPlay.Play();
                    playingSoundList.Add(soundToPlay);
                }
            }
            else
            {
                Debug.LogError(soundName + " not found in Resources");
            }
            return playingSoundList;
    }
    

    private static List<AudioSource> StopSound(string soundName, Dictionary<string, GameObject> dictionary, List<AudioSource> playingSoundList)
    {
        if (dictionary.TryGetValue(soundName, out GameObject objectToPlay))
        {
            AudioSource soundToStop = objectToPlay.GetComponent<AudioSource>();
            if (playingSoundList.Contains(soundToStop))
            {
                soundToStop.Stop();
                playingSoundList.Remove(soundToStop);
            }
            else
            {
                Debug.LogError(soundName + " is not playing !");
            }
        }
        else
        {
            Debug.LogError(soundName + " not found in Resources");
        }

        return playingSoundList;
    }
}
