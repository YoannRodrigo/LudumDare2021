using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject soundMenu;

    private GameObject lastMenu;
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
        }
    }

    private void Start()
    {
        lastMenu = startMenu;
    }

    public void OnPauseInGame()
    {
        lastMenu = inGameMenu;
        inGameMenu.SetActive(!inGameMenu.activeSelf);
    }

    public void OnStartGame()
    {
        startMenu.SetActive(false);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void OnResumeGame()
    {
        inGameMenu.SetActive(false);
    }

    public void OnBackMenu()
    {
        soundMenu.SetActive(false);
        lastMenu.SetActive(true);
    }

    public void OnSoundMenu()
    {
        lastMenu.SetActive(false);
        soundMenu.SetActive(true);
    }
}
