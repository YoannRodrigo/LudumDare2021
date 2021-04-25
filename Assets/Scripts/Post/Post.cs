using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace post
{
    public class Post : MonoBehaviour
    {
        [SerializeField] private PostData _infos;
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _username;
        [SerializeField] private TextMeshProUGUI _date;
        [SerializeField] private TextMeshProUGUI _textContent;
        [SerializeField] private GameObject _model3D;
        [SerializeField] private SceneReference _modelViewerScene;
        [SerializeField] private GameObject _blackBackground;
        [SerializeField] private PostReaction _reactions;

        private void Awake()
        {
            FillPostWithInfos(_infos);
        }
        public void FillPostWithInfos(PostData infos)
        {
            _infos = infos;
            _username.text = infos.Username;
            _date.text = infos.Date;
            FillTextContent(infos.Content.Text);
            FillImageContent(infos.Content.Image);
            _reactions.SetReaction(_infos.HasPlayerReacted, _infos.ReactionAmount);
        }

        private void FillTextContent(string text)
        {
            if (text != "")
            {
                _textContent.text = text;
                _textContent.enabled = true;
            }
            else
            {
                _textContent.enabled = false;
            }
        }

        private void FillImageContent(Sprite image)
        {
            if (image != null)
            {
                _image.sprite = image;
                _image.enabled = true;
            }
            else
            {
                _image.enabled = false;
            }
        }

        private void FillModel3DContent(GameObject model3D)
        {
            if (model3D != null)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(_modelViewerScene, LoadSceneMode.Additive);
                operation.completed += (op) => ModelViewer.Instance.DisplayModel(model3D);
            }
            else
            {
                _model3D.SetActive(false);
            }
        }
        public void OpenModelViewer()
        {
            if (_infos.Content.model3D != null)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(_modelViewerScene, LoadSceneMode.Additive);
                operation.completed += (op) =>
                {
                    ModelViewer.Instance.DisplayModel(_infos.Content.model3D);
                    _blackBackground.SetActive(true);
                };
            }
        }
        public void CloseViewer()
        {
            _blackBackground.SetActive(false);
            ModelViewer.Instance.CloseViewer();
        }

        public void ReactToPost()
        {
            _infos.HasPlayerReacted = !_infos.HasPlayerReacted;
            _infos.ReactionAmount = _infos.HasPlayerReacted ? _infos.ReactionAmount + 1 : _infos.ReactionAmount - 1;
            _reactions.SetReaction(_infos.HasPlayerReacted, _infos.ReactionAmount);
        }
    }
}