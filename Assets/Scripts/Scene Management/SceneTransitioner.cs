using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using player;
using camera;
using System;

namespace levelManagement
{
    public class SceneTransitioner : MonoBehaviour
    {
        public static SceneTransitioner Instance;
        [SerializeField] private SceneReference _mainMenu;
        private PlayerData _playerData;

        [SerializeField] private Level _currentLevel;
        [SerializeField] private Level _nextLevel;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void TransitionToNextScene()
        {
            CloseScene(_currentLevel.Scene);

            if (_currentLevel.NextLevel.ScenePath == "")
            {
                LoadMainMenu();
                return;
            }
            else
            {
                _currentLevel = _nextLevel;
                if (_currentLevel.NextLevel.ScenePath == "")
                {
                    return;
                }
                LoadNextLevel(_currentLevel.NextLevel);
            }
        }

        public void CloseScene(SceneReference sceneToClose)
        {
            AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneToClose);
        }

        private void LoadMainMenu()
        {
            LoadNextScene(_mainMenu, () => DeletePersistentGameObject());
        }

        private void LoadNextScene(SceneReference sceneToload, Action actionOnLoad = null)
        {
            AsyncOperation operation = LoadScene(sceneToload);
            if (actionOnLoad == null)
            {
                return;
            }
            operation.completed += (ope) => actionOnLoad();
        }

        public void LoadNextLevel(SceneReference sceneToload)
        {
            AsyncOperation operation = LoadScene(sceneToload);
            operation.completed += (asyncOperation) => _nextLevel = GetLevelInScene(sceneToload);
        }

        private AsyncOperation LoadScene(SceneReference sceneReference)
        {
            return SceneManager.LoadSceneAsync(sceneReference, LoadSceneMode.Additive);
        }

        public void LoadFirstLevel(SceneReference sceneReference)
        {
            AsyncOperation operation = LoadScene(sceneReference);
            operation.completed += (asyncOperation) =>
            {
                _currentLevel = GetLevelInScene(sceneReference);
                if (_currentLevel == null)
                {
                    Debug.Log("level");
                    return;
                }
                AsyncOperation operation = LoadScene(_currentLevel.NextLevel);
                operation.completed += (asyncOperation) =>
                {
                    _nextLevel = GetLevelInScene(_currentLevel.NextLevel);
                };
            };
        }
        private Level GetLevelInScene(SceneReference sceneReference)
        {
            Scene scene = SceneManager.GetSceneByPath(sceneReference.ScenePath);
            Level[] levels = GameObject.FindObjectsOfType<Level>();
            foreach (Level level in levels)
            {
                if (level.Scene.ScenePath == sceneReference.ScenePath)
                {
                    return level;
                }
            }
            Debug.LogError("Cant find Gameobject of type Level");
            return null;
        }

        private void DeletePersistentGameObject()
        {
            Destroy(PersistentCamera.Instance.gameObject);
            Destroy(Movement.Instance.gameObject);
        }

    }
}



