using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private List<GameObject> mainVolumeBars;
    [SerializeField] private List<GameObject> musicVolumeBars;
    [SerializeField] private List<GameObject> sfxVolumeBars;

    private int nbMainVolumeBars = 20;
    private int nbMusicVolumeBars = 20;
    private int nbSFXVolumeBars = 20;
    
    public void OnPauseInGame()
    {
        soundMenu.SetActive(!soundMenu.activeSelf);
    }

    public void OnMainVolumeClick(int value)
    {
        nbMainVolumeBars = Mathf.Clamp(nbMainVolumeBars + value, 0, 20);
        SoundManager.UpdateMainVolume(nbMainVolumeBars/20f);
        int i;
        for (i = 0; i < nbMainVolumeBars; i++)
        {
            mainVolumeBars[i].SetActive(true);
        }
        for (i = nbMainVolumeBars; i < mainVolumeBars.Count; i++)
        {
            mainVolumeBars[i].SetActive(false);
        }
    }
    
    public void OnMusicVolumeClick(int value)
    {
        nbMusicVolumeBars = Mathf.Clamp(nbMusicVolumeBars + value, 0, 20);
        SoundManager.UpdateMainVolume(nbMusicVolumeBars/20f);
        int i;
        for (i = 0; i < nbMusicVolumeBars; i++)
        {
            musicVolumeBars[i].SetActive(true);
        }
        for (i = nbMusicVolumeBars; i < musicVolumeBars.Count; i++)
        {
            musicVolumeBars[i].SetActive(false);
        }
    }
    
    public void OnSFXVolumeClick(int value)
    {
        nbSFXVolumeBars = Mathf.Clamp(nbSFXVolumeBars + value, 0, 20);
        SoundManager.UpdateMainVolume(nbSFXVolumeBars/20f);
        int i;
        for (i = 0; i < nbSFXVolumeBars; i++)
        {
            sfxVolumeBars[i].SetActive(true);
        }
        for (i = nbSFXVolumeBars; i < sfxVolumeBars.Count; i++)
        {
            sfxVolumeBars[i].SetActive(false);
        }
    }
    
    
}
