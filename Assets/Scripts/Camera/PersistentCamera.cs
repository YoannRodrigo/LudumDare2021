using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace camera
{
    public class PersistentCamera : MonoBehaviour
    {
        public static PersistentCamera Instance;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private CinemachineVirtualCamera _cameraInUse;

        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }

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