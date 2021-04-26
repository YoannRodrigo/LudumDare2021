using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace post
{
    public class PostFeed : MonoBehaviour
    {
        [SerializeField] private WebSiteManager _websiteManager;
        [SerializeField] private SceneReference _modelViewerScene;
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private BlackBackground _blackBackground;
        [SerializeField] private float _feedSize;
        private float _feedStartHeight;
        private bool _isSetuped = false;

        public float FeedSize { get => _feedSize; private set => _feedSize = value; }
        public float FeedStartHeight { get => _feedStartHeight; private set => _feedStartHeight = value; }
        public bool IsSetuped
        {
            get => _isSetuped;
            private set
            {
                _isSetuped = value;
                OnSetupFinished?.Invoke();
            }
        }

        public event Action OnSetupFinished;

        private void Start()
        {
            StartCoroutine(Layout());
        }

        private IEnumerator Layout()
        {
            _layoutGroup.enabled = false;
            yield return new WaitForEndOfFrame();
            _layoutGroup.enabled = true;
            yield return new WaitForEndOfFrame();
            _feedSize = GetComponent<RectTransform>().sizeDelta.y;
            _feedStartHeight = GetComponent<RectTransform>().anchoredPosition.y;
            _layoutGroup.enabled = false;
            IsSetuped = true;

        }

        public void OpenModelViewer(GameObject model3D)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_modelViewerScene, LoadSceneMode.Additive);
            operation.completed += (op) =>
            {
                _websiteManager.HasObjectViewerOpen = true;
                ModelViewer.Instance.DisplayModel(model3D);
                _blackBackground.OpenBackground(() =>
                {
                    ModelViewer.Instance.CloseViewer();
                    _websiteManager.HasObjectViewerOpen = false;
                });
            };
        }
    }
}