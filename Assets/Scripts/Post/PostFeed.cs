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
        [SerializeField] private Post[] _posts;

        private void Start()
        {
            StartCoroutine(Layout());
        }

        private IEnumerator Layout()
        {
            _layoutGroup.enabled = false;
            yield return null;
            _layoutGroup.enabled = true;
            yield return null;
            _layoutGroup.enabled = false;
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