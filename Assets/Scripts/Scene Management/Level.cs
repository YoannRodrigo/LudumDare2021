using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace levelManagement
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private SceneReference _scene;
        [SerializeField] private SceneReference _nextLevel;
        [SerializeField] private CinemachineVirtualCamera _levelCamera;

        public SceneReference Scene { get => _scene; set => _scene = value; }
        public CinemachineVirtualCamera LevelCamera { get => _levelCamera; set => _levelCamera = value; }
        public SceneReference NextLevel { get => _nextLevel; set => _nextLevel = value; }

        private void Start()
        {
        }

        public void TransitionToNextLevel()
        {
            SceneTransitioner.Instance.TransitionToNextScene();
        }
        public void CloseLevel()
        {
            SceneTransitioner.Instance.CloseScene(Scene);
        }
    }
}



