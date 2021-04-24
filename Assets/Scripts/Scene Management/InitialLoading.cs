using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace levelManagement
{
    public class InitialLoading : MonoBehaviour
    {
        [SerializeField] private SceneReference _mainMenu;

        private void Awake()
        {
            LoadMenu();
        }
        public void LoadMenu()
        {
            SceneManager.LoadScene(_mainMenu,LoadSceneMode.Additive);
        }
    }
}