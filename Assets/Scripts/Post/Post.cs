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
        [SerializeField] private GameObject _containerMedia;
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _username;
        [SerializeField] private TextMeshProUGUI _date;
        [SerializeField] private TextMeshProUGUI _textContent;
        [SerializeField] private SceneReference _modelViewerScene;
        [SerializeField] private BlackBackground _blackBackground;
        [SerializeField] private PostReaction _reactions;
        [SerializeField] private TextMeshProUGUI _commentsAmount;
        [SerializeField] private TextMeshProUGUI _repweetsAmount;

        private void OnValidate()
        {
            FillPostWithInfos();
        }

        public void FillPostWithInfos()
        {
            _username.text = _infos.Username;
            _avatar.sprite = _infos.Avatar;
            _date.text = _infos.Date;
            FillTextContent(_infos.Content.Text);
            FillImageContent(_infos.Content.Image);
            _reactions.SetReaction(_infos.HasPlayerReacted, _infos.ReactionAmount);
            _commentsAmount.text = _infos.CommentsAmount.ToString();
            _repweetsAmount.text = _infos.RepweetAmount.ToString();
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
            if (image != default(Sprite))
            {
                _image.sprite = image;
                _image.enabled = true;
                _containerMedia.SetActive(true);
            }
            else
            {
                _containerMedia.SetActive(false);
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
        }
        public void OpenModelViewer()
        {
            if (_infos.Content.model3D != null)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(_modelViewerScene, LoadSceneMode.Additive);
                operation.completed += (op) =>
                {
                    ModelViewer.Instance.DisplayModel(_infos.Content.model3D);
                    _blackBackground.OpenBackground(() => ModelViewer.Instance.CloseViewer());
                };
            }
        }
        public void CloseViewer()
        {
            _blackBackground.CloseBackground();
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