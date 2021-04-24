using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace levelManagement
{
    public class LevelBorder : MonoBehaviour
    {

        [SerializeField] private Level _level;
        private void OnBecameInvisible()
        {
            _level.TransitionToNextLevel();
        }
    }
}