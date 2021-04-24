using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        public static CameraSwitcher Instance;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private CinemachineVirtualCamera _cameraInUse;

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
    }
}