using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace levelManagement
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private SceneReference _scene;
        [SerializeField] private SceneReference _firstLevel;
        public void LoadFirstLevel()
        {
            SceneTransitioner.Instance.LoadFirstLevel(_firstLevel);
            AsyncOperation operation  =SceneManager.UnloadSceneAsync(_scene.ScenePath);
        }
    }
}